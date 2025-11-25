using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
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

    public StateMachine<BattleSystem> FSM { get; private set; }

    private void Awake()
    {
        Instance = this;
        UI.gameObject.SetActive(false);
    }

    private void Update()
    {
        FSM?.Update();
    }

    public void StartBattle(EnemySO enemy)
    {
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
        EventManager.Instance.AddListener<BattleEndedEvent>(OnBattleEnded);
    }

    private void OnBattleEnded(BattleEndedEvent e)
    {
        // Setup items
        foreach (var item in GameManager.Instance.Player.Inventory.GetConsumableItems())
        {
            (item.Effect as ConsumableItem).BattleEnded();
        }

        EventManager.Instance.RemoveListener<BattleEndedEvent>(OnBattleEnded);
    }

    public void Attack(Unit source, Unit target)
    {
        source.Attack();

        float attack = source.Character.Stats["attack_phy"].Value;
        float defense = target.Character.Stats["defense_phy"].Value;

        float damage = CalculateDamage(attack, defense);

        DealDamage(source, target, damage);
    }

    public void Magic(Unit source, Unit target, float attackValue, float defenseValue)
    {
        float damage = CalculateDamage(attackValue, defenseValue);

        DealDamage(source, target, damage);
    }

    public void Escape(Unit source, float chance)
    {
        float rng = Random.Range(0f, 1f);
        Dictionary<string, Unit> args = new Dictionary<string, Unit>()
        {
            {"name", source }
        };

        if (rng <= chance)
        {
            UI.TextBox.SetText(escapeSuccess.GetLocalizedString(args)).OnComplete(() =>
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
            UI.TextBox.SetText(escapeFail.GetLocalizedString(args)).OnComplete(() =>
            {
                switch (FSM.currentState)
                {
                    case PlayerTurnState:
                        (FSM.currentState as PlayerTurnState).EndTurn(1.5f);
                        break;
                    case EnemyTurnState:
                        (FSM.currentState as EnemyTurnState).EndTurn(1.5f);
                        break;
                }
            });
        }
    }

    public void Heal(Unit character, float value)
    {
        character.Heal(value);
    }

    public void DealDamage(Unit source, Unit target, float damage)
    {
        target.TakeDamage(source, damage);
    }

    public float CalculateDamage(float attack, float defense)
    {
        float dmg = attack - (defense / 2);
        return Mathf.Clamp(dmg, 1, float.MaxValue);
    }
}
