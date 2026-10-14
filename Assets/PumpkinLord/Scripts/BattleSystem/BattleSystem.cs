using DG.Tweening;
using QFSW.QC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using static UnityEngine.UI.GridLayoutGroup;
using Random = UnityEngine.Random;

public class BattleSystem : MonoBehaviour
{
    public static BattleSystem Instance;

    [SerializeField] private EnemySO testEnemy;

    [Header("Units")]
    [field:SerializeField] public Unit PlayerUnit { get; private set; }
    [field:SerializeField] public Unit EnemyUnit { get; private set; }

    [Header("HUD")]
    [field: SerializeField] public BattleUI UI { get; private set; }
    [SerializeField] private LocalizedString escapeSuccess;
    [SerializeField] private LocalizedString escapeFail;

    [Header("Textes")]
    [SerializeField] private LocalizedString attackMissText;
    [SerializeField] private LocalizedString attackDodgedText;

    public StateMachine<BattleSystem> FSM { get; private set; }
    public bool IsBoss { get; private set; }
    public Zone CurrentZone { get; private set; }

    private void Awake()
    {
        Instance = this;
        UI.gameObject.SetActive(false);
    }

    private void Update()
    {
        FSM?.Update();
    }

    public void StartBattle(EnemySO enemy, bool isBoss)
    {
        IsBoss = isBoss;

        // Setup enemy
        var Enemy = Instantiate(enemy.Enemy, EnemyUnit.transform);
        Enemy.Setup(EnemyUnit);
        EnemyUnit.UnitName = enemy.GetName();
        EnemyUnit.Character.BaseStats = enemy.BaseStats;

        // Start battle
        FSM = new StateMachine<BattleSystem>(this);
        FSM.ChangeState(new StartBattleState());

        UI.gameObject.SetActive(true);

        // Setup items
        foreach (var item in GameManager.Instance.Player.Inventory.GetConsumableItems())
        {
            (item.Effect as ConsumableItem).BattleStart();
        }

        PlayerUnit.Status.BattleStart();
        EnemyUnit.Status.BattleStart();

        EventManager.Instance.AddListener<BattleEndedEvent>(OnBattleEnded);
        GameManager.Instance.GetComponent<PauseSystem>().CanPause = false;
        GameManager.Instance.Input.ChangeContext(InputManager.InputContext.UserInterface);
    }

    private void OnBattleEnded(BattleEndedEvent e)
    {
        // Setup items
        foreach (var item in GameManager.Instance.Player.Inventory.GetConsumableItems())
        {
            (item.Effect as ConsumableItem).BattleEnded();
        }

        PlayerUnit.Status.BattleEnd();
        EnemyUnit.Status.BattleEnd();

        EventManager.Instance.RemoveListener<BattleEndedEvent>(OnBattleEnded);
        GameManager.Instance.GetComponent<PauseSystem>().CanPause = true;
        GameManager.Instance.Input.ChangeContext(InputManager.InputContext.Default);
    }

    public void Escape(Unit source, float chance)
    {
        float rng = Random.Range(0f, 1f);
        Dictionary<string, Unit> args = new()
        {
            {"name", source }
        };

        if (rng <= chance)
        {
            UI.TextBox.SetText(escapeSuccess.GetLocalizedString(args), () =>
            {
                GameManager.Instance.UnloadAdditiveScene(() =>
                {
                    GameManager.Instance.Board.SetActive(true);
                    EventManager.Instance.TriggerEvent(new BattleEndedEvent());
                });
            });
        }
        else
        {
            UI.TextBox.SetText(escapeFail.GetLocalizedString(args), () =>
            {
                (FSM.currentState as UnitTurnState).EndTurn(1.5f);
            });
        }
    }

    public void DealDamage(DamageInfo info)
    {
        float missRoll = Random.Range(0f, 1f);
        if (missRoll <= info.Source.Character.Stats["miss_chance"].Value)
        {
            UI.TextBox.SetText("Missgadjgnaodfgnaodfgnoadfgnaodfgnoadfgnoadsfngodafgnoadfgnsd");
        }
        else
        {
            missRoll = Random.Range(0f, 1f);

            if (missRoll <= info.Target.Character.Stats["dodge_chance"].Value)
                UI.TextBox.SetText(info.Target.UnitName + " dodged.");
            else
                info.Target.TakeDamage(info);
        }
    }

    public void AddZone(Zone zoneToAdd)
    {
        CloseZone();

        var zone = Instantiate(zoneToAdd, Vector3.zero, Quaternion.identity);
        CurrentZone = zone;
        CurrentZone.Effect.OpenZone();
        EventManager.Instance.TriggerEvent(new OpenZoneEvent() { Zone = CurrentZone });
    }

    public void CloseZone()
    {
        if (CurrentZone != null)
        {
            CurrentZone.Effect.CloseZone();
            EventManager.Instance.TriggerEvent(new CloseZoneEvent() { Zone = CurrentZone });
            Destroy(CurrentZone.gameObject);
            CurrentZone = null;
        }
    }
}
