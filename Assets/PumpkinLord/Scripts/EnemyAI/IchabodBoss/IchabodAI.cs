using DG.Tweening;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UIElements;

[System.Serializable]
public class IchabodAI : EnemyAI
{
    [Header("Slash")]
    [SerializeField] private LocalizedString slashName;
    [SerializeField] private LocalizedString slashDesc;
    [SerializeField] private LocalizedString slashText;
    [SerializeField] private float slashWeight = 50f;
    [SerializeField] private float damage;

    [Header("Lunge")]
    [SerializeField] private LocalizedString lungeName;
    [SerializeField] private LocalizedString lungeDesc;
    [SerializeField] private LocalizedString lungeText;
    [SerializeField] private float lungeWeight = 10f;
    [SerializeField] private float lungeDamageMultiplier = 0.5f;
    [SerializeReference] private StatusEffect lungeStagger;

    [Header("Poison")]
    [SerializeField] private LocalizedString fireballName;
    [SerializeField] private LocalizedString fireballDesc;
    [SerializeField] private LocalizedString fireballText;
    [SerializeField] private float fireballWeight = 40f;
    [SerializeField] private float fireballCost = 5f;

    [Header("Heal")]
    [SerializeField] private LocalizedString healName;
    [SerializeField] private LocalizedString healDesc;
    [SerializeField] private LocalizedString healText;
    [SerializeField] private float healWeight = 30f;
    [SerializeField] private float healCost = 10f;
    [SerializeField, Range(0, 1)] private float healValue = 0.1f;

    public override void EvaluateNextAction()
    {
        WeightedRandomSelector<string> options = new();

        options.AddOption(nameof(Slash), slashWeight);
        options.AddOption(nameof(Lunge), lungeWeight);

        if (Owner.Unit.Character.ManaPoints.Value >= fireballCost)
        {
            options.AddOption(nameof(Fireball), fireballWeight);
        }
        if (Owner.Unit.Character.ManaPoints.Value >= healCost)
        {
            options.AddOption(nameof(Heal), healWeight);
        }

        string selection = options.SelectRandom();

        switch (selection)
        {
            case nameof(Slash):
                Slash();
                break;
            case nameof(Lunge):
                Lunge();
                break;
            case nameof(Fireball):
                Fireball();
                break;
            case nameof(Heal):
                Heal();
                break;
            default:
                throw new System.ArgumentOutOfRangeException("Function not found");
        }
    }

    public override string GetAttacksDescription()
    {
        return $"<b>{slashName.GetLocalizedString()}</b> - {slashDesc.GetLocalizedString()}\n\n" +
            $"<b>{lungeName.GetLocalizedString()}</b> - {lungeDesc.GetLocalizedString()}\n\n" +
            $"<b>{fireballName.GetLocalizedString()}</b> - {fireballDesc.GetLocalizedString()}\n\n" +
            $"<b>{healName.GetLocalizedString()}</b> - {healDesc.GetLocalizedString()}";
    }

    public void Slash()
    {
        BattleSystem battleSystem = Owner.Unit.BattleSystem;
        battleSystem.UI.TextBox.SetText(slashText.GetLocalizedString()).OnComplete(() =>
        {
            Owner.Unit.PhysAttack(battleSystem.PlayerUnit, 0.5f);
            var enemyTurn = battleSystem.FSM.currentState as EnemyTurnState;
            enemyTurn.EndTurn(1.5f);
        });
    }

    public void Lunge()
    {
        BattleSystem battleSystem = Owner.Unit.BattleSystem;
        battleSystem.UI.TextBox.SetText(slashText.GetLocalizedString()).OnComplete(() =>
        {
            Owner.Unit.PhysAttack(battleSystem.PlayerUnit, 0.5f);
            battleSystem.PlayerUnit.Status.AddStatusEffect(lungeStagger);

            var enemyTurn = battleSystem.FSM.currentState as EnemyTurnState;
            enemyTurn.EndTurn(1.5f);
        });
    }

    public void Fireball()
    {
        BattleSystem battleSystem = Owner.Unit.BattleSystem;

        Unit player = battleSystem.PlayerUnit;
        Unit enemy = Owner.Unit;

        enemy.Character.ManaPoints.Value -= fireballCost;

        battleSystem.UI.TextBox.SetText(fireballText.GetLocalizedString()).OnComplete(() =>
        {
            enemy.MagicAttack(player);

            var enemyTurn = battleSystem.FSM.currentState as EnemyTurnState;
            enemyTurn.EndTurn(1.5f);
        });
    }

    public void Heal()
    {
        BattleSystem battleSystem = Owner.Unit.BattleSystem;
        battleSystem.UI.TextBox.SetText(healText.GetLocalizedString()).OnComplete(() =>
        {
            Owner.Unit.Heal(Owner.Unit.Character.HealthPoints.Value * healValue);
            Owner.Unit.Character.ManaPoints.Value -= healCost;

            var enemyTurn = battleSystem.FSM.currentState as EnemyTurnState;
            enemyTurn.EndTurn(1.5f);
        });
    }
}
