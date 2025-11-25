using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;

[System.Serializable]
public class PumpkinMode : BoardGameMode
{
    [Header("Battle Setup")]
    [SerializeField] private AssetReference[] scenes;
    [SerializeField] private EnemySO[] enemies;

    [Header("Board Setup")]
    [SerializeField] private BoxTypeEffect[] boxTypes;
    [SerializeField] private List<int> bonFireIDs;
    [SerializeField] private BoxSO bonfireBox;

    [Header("Dices")]
    [SerializeField] private Dice[] dices;
    private int[] diceThrows;
    private bool diceLaunched;

    [SerializeField] private GameObject moveVolume;
    [SerializeField] private GameObject diceCamera;

    private int playerPosition;
    private int boxesToMove;

    public override void InitializeBoard()
    {
        var bonFireBoxes = boxes.Where((n, i) => (bonFireIDs.Contains(i))).ToList();
        bonFireBoxes.ForEach(o => o.Initialize(bonfireBox));

        List<BoxSO> boxesSO = new();

        foreach (var boxEffect in boxTypes)
        {
            for (int i = 0; i < boxEffect.MaxQuantity; i++)
            {
                boxesSO.Add(boxEffect.Type);
            }
        }

        var shuffledBoxes = boxes.Where((n, i) => (!bonFireIDs.Contains(i))).ToList();
        shuffledBoxes.Shuffle();

        Queue<BoardBox> randomBoxes = new();
        shuffledBoxes.ForEach(o => randomBoxes.Enqueue(o));

        foreach (var box in boxesSO)
        {
            randomBoxes.Dequeue().Initialize(box);
        }
    }

    public override void MovePlayerToNextPosition()
    {
        if (boxesToMove > 0)
        {
            boxesToMove--;
            EventManager.Instance.TriggerEvent(new MoveEvent { RemainingBoxes = boxesToMove });
            playerPosition++;
            playerPawn.MoveTo(GetBox().LandingPoint);
        }
        else
        {
            // Box land logic
            diceCamera.SetActive(true);
            moveVolume.SetActive(false);
            GetBox().OnLanded?.Invoke();
            EventManager.Instance.TriggerEvent(new BoxLandEvent());
        }
    }

    public override BoardBox GetBox()
    {
        if (playerPosition >= boxes.Length)
            playerPosition = 0;

        return boxes[playerPosition];
    }

    public override void GetNextBoxOfType(Type box)
    {
        int nextPosition = -1;

        for (int i = 0; i < boxes.Length; i++)
        {
            if (i <= playerPosition)
                continue;

            if (boxes[i].Effect.GetType() == box)
            {
                nextPosition = i;
                break;
            }
        }

        if (nextPosition == -1)
        {
            for (int i = 0; i < boxes.Length; i++)
            {
                if (boxes[i].Effect.GetType() == box)
                {
                    nextPosition = i;
                    break;
                }
            }
        }

        int boxesToMove;
        if (playerPosition > nextPosition)
        {
            boxesToMove = nextPosition +  boxes.Length - playerPosition;
        }
        else
        {
            boxesToMove = nextPosition - playerPosition;
        }

        MovePlayer(boxesToMove);
    }

    public override void ThrowDices()
    {
        if (diceLaunched) return;

        diceThrows = new int[dices.Length];
        diceLaunched = true;

        foreach (var dice in dices)
            dice.Shoot();

        EventManager.Instance.TriggerEvent(new DiceThrownEvent());
    }

    public override void SetDiceThrow(int diceId, int diceThrow)
    {
        if (!diceLaunched) return;

        if (diceThrows[diceId] != 0)
            return;

        diceThrows[diceId] = diceThrow;

        bool threwAllDices = true;

        for (int i = 0; i < diceThrows.Length;i++)
        {
            if (dices[i].IsRolling)
                threwAllDices = false;
        }

        if (threwAllDices)
        {
            diceLaunched = false;

            int result = 0;
            foreach (var number in diceThrows)
                result += number;

            diceCamera.SetActive(false);
            moveVolume.SetActive(true);

            //Move the player
            boxesToMove = result;
            MovePlayerToNextPosition();
        }
    }

    public override void StartBattle(EnemySO enemy)
    {
        EventManager.Instance.TriggerEvent(new StartBattleEvent() { Enemy = enemy });

        //Select random battle scene
        int sceneId = Random.Range(0, scenes.Length);
        AssetReference scene = scenes[sceneId];

        var loadHandle = GameManager.Instance.LoadAdditiveScene(scene,
            () =>
            {
                GameManager.Instance.Board.SetActive(false);
            },
            () =>
            {
                BattleSystem.Instance.StartBattle(enemy);
            }
        );
    }

    public override void StartBattle(string enemyId)
    {
        foreach (EnemySO enemy in enemies)
        {
            if (enemy.name == enemyId)
            {
                StartBattle(enemy);
                return;
            }
        }
    }

    public void MovePlayer(int steps)
    {
        EventManager.Instance.TriggerEvent(new DiceThrownEvent());

        diceCamera.SetActive(false);
        moveVolume.SetActive(true);

        boxesToMove = steps;
        MovePlayerToNextPosition();
    }
}

[System.Serializable]
public class BoxTypeEffect
{
    public BoxSO Type;
    public int MaxQuantity;
}
