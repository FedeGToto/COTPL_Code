using DG.Tweening;
using QFSW.QC.Actions;
using UnityEngine;

public class ManaRestoreSpell : Spell
{
    [SerializeField] private bool isPercentage;
    [SerializeField] private float restoreValue;

    public override void Use(Unit caster)
    {
        UseCost(caster);

        BattleSystem.Instance.UI.TextBox.SetText(magicText.GetLocalizedString(), () =>
        {
            foreach (var target in GetTargets())
            {
                float value = restoreValue;

                if (isPercentage)
                {
                    value = caster.Character.ManaPoints.Value * restoreValue;
                }
                caster.Heal(value * Effectiveness);
            }

            var turn = BattleSystem.Instance.FSM.currentState as UnitTurnState;
            turn.EndTurn(1.5f);
        });
    }

    public override bool CostCheck(Unit caster)
    {
        return ManaCheck(caster);
    }

    public override void UseCost(Unit caster)
    {
        ManaUse(caster);
    }
}
