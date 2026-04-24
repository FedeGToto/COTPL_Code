using DG.Tweening;
using UnityEngine;
using UnityEngine.Localization;

public class DoctorAI : EnemyAI
{
    [Header("Scalpel")]
    [SerializeField] private LocalizedString scalpelName;
    [SerializeField] private LocalizedString scalpelDesc;
    [SerializeField] private LocalizedString scalpelText;
    [SerializeField] private float scalpelWeight = 50f;

    [Header("Poison")]
    [SerializeField] private LocalizedString poisonName;
    [SerializeField] private LocalizedString poisonDesc;
    [SerializeField] private LocalizedString poisonText;
    [SerializeField] private float poisonWeight = 30f;
    [SerializeReference] private StatusEffect poisonStatus;
    [SerializeField] private float poisonCost = 5f;

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

        options.AddOption(nameof(Scalpel), scalpelWeight);

        if (Owner.Unit.Character.ManaPoints.Value >= poisonCost)
        {
            options.AddOption(nameof(Poison), poisonWeight);
        }
        if (Owner.Unit.Character.ManaPoints.Value >= healCost)
        {
            options.AddOption(nameof(Heal), healWeight);
        }

        string selection = options.SelectRandom();

        switch (selection)
        {
            case nameof(Scalpel):
                Scalpel();
                break;
            case nameof(Poison):
                Poison();
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
        return $"<b>{scalpelName.GetLocalizedString()}</b> - {scalpelDesc.GetLocalizedString()}\n\n" +
            $"<b>{poisonName.GetLocalizedString()}</b> - {poisonDesc.GetLocalizedString()}\n\n" +
            $"<b>{healName.GetLocalizedString()}</b> - {healDesc.GetLocalizedString()}";
    }

    public void Scalpel()
    {
        BattleSystem battleSystem = Owner.Unit.BattleSystem;
        battleSystem.UI.TextBox.SetText(scalpelText.GetLocalizedString()).OnComplete(() =>
        {
            Owner.Unit.PhysAttack(battleSystem.PlayerUnit);
            var enemyTurn = battleSystem.FSM.currentState as EnemyTurnState;
            enemyTurn.EndTurn(1.5f);
        });
    }

    public void Poison()
    {
        BattleSystem battleSystem = Owner.Unit.BattleSystem;
        battleSystem.UI.TextBox.SetText(poisonText.GetLocalizedString()).OnComplete(() =>
        {
            battleSystem.PlayerUnit.Status.AddStatusEffect(poisonStatus);
            Owner.Unit.Character.ManaPoints.Value -= poisonCost;

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
