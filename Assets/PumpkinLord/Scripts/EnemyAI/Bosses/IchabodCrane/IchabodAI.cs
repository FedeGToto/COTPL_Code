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

    [Header("Fireball")]
    [SerializeField] private float fireballWeight = 40f;
    [SerializeReference] private Spell fireball;

    [Header("Heal")]
    [SerializeField] private float healWeight = 30f;
    [SerializeReference] private Spell heal;

    public override void EvaluateNextAction()
    {
        WeightedRandomSelector<string> options = new();

        options.AddOption(nameof(Slash), slashWeight);
        options.AddOption(nameof(Lunge), lungeWeight);

        if (Owner.Unit.Character.ManaPoints.Value >= fireball.RealCost)
        {
            options.AddOption(nameof(Fireball), fireballWeight);
        }
        if (Owner.Unit.Character.ManaPoints.Value >= heal.RealCost)
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
            $"<b>{fireball.SpellName.GetLocalizedString()}</b> - {fireball.SpellDescription.GetLocalizedString()}\n\n" +
            $"<b>{heal.SpellName.GetLocalizedString()}</b> - {heal.SpellDescription.GetLocalizedString()}";
    }

    public void Slash()
    {
        BattleSystem battleSystem = Owner.Unit.BattleSystem;
        battleSystem.UI.TextBox.SetText(slashText.GetLocalizedString(), () =>
        {
            Owner.Unit.PhysAttack(battleSystem.PlayerUnit, 0.5f);
            var enemyTurn = battleSystem.FSM.currentState as EnemyTurnState;
            enemyTurn.EndTurn(1.5f);
        });
    }

    public void Lunge()
    {
        BattleSystem battleSystem = Owner.Unit.BattleSystem;
        battleSystem.UI.TextBox.SetText(slashText.GetLocalizedString(), () =>
        {
            Owner.Unit.PhysAttack(battleSystem.PlayerUnit, 0.5f, type: AttackType.Spell);
            battleSystem.PlayerUnit.Status.AddStatusEffect(lungeStagger);

            var enemyTurn = battleSystem.FSM.currentState as EnemyTurnState;
            enemyTurn.EndTurn(1.5f);
        });
    }

    public void Fireball()
    {
        BattleSystem battleSystem = Owner.Unit.BattleSystem;
        fireball.Use(battleSystem.EnemyUnit);
    }

    public void Heal()
    {
        BattleSystem battleSystem = Owner.Unit.BattleSystem;
        heal.Use(battleSystem.EnemyUnit);
    }
}
