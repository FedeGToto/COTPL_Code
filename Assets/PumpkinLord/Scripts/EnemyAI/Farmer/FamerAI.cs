using DG.Tweening;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UIElements;

public class FamerAI : EnemyAI
{
    [Header("Pitchfork")]
    [SerializeField] private LocalizedString attackName;
    [SerializeField] private LocalizedString attackDescription;
    [SerializeField] private LocalizedString attackText;
    [SerializeField] private float pitchforkWeight = 70f;

    [Header("Escape")]
    [SerializeField] private LocalizedString escapeName;
    [SerializeField] private LocalizedString escapeDescription;
    [SerializeField] private LocalizedString escapeText;
    [SerializeField] private float escapeWeight = 30f;
    [SerializeField] private float fleeChance = 0.1f;

    public override void EvaluateNextAction()
    {
        WeightedRandomSelector<string> options = new();

        options.AddOption(nameof(Pitchfork), pitchforkWeight);
        options.AddOption(nameof(Escape), escapeWeight);

        string selection = options.SelectRandom();

        switch (selection)
        {
            case nameof(Pitchfork):
                Pitchfork();
                break;
            case nameof(Escape):
                Escape();
                break;
            default:
                throw new System.ArgumentOutOfRangeException("Function not found");
        }
    }

    public override string GetAttacksDescription()
    {
        return $"<b>{attackName.GetLocalizedString()}</b> - {attackDescription.GetLocalizedString()}\n\n" +
            $"<b>{escapeName.GetLocalizedString()}</b> - {escapeDescription.GetLocalizedString()}";
    }

    public void Pitchfork()
    {
        BattleSystem battleSystem = Owner.Unit.BattleSystem;
        battleSystem.UI.TextBox.SetText(attackText.GetLocalizedString()).OnComplete(() =>
        {
            Owner.Unit.PhysAttack(battleSystem.PlayerUnit);
            var enemyTurn = battleSystem.FSM.currentState as EnemyTurnState;
            enemyTurn.EndTurn(1.5f);
        });

    }

    public void Escape()
    {
        Owner.Unit.BattleSystem.UI.TextBox.SetText(escapeText.GetLocalizedString()).OnComplete(() =>
        {
            Owner.Unit.BattleSystem.Escape(Owner.Unit, fleeChance);
        });
    }
}
