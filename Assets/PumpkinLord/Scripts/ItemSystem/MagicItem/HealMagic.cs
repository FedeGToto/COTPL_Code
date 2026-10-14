using DG.Tweening;
using Rewired;
using UnityEngine;
using UnityEngine.Localization;

[System.Serializable]
public class HealMagic : Spell
{
    [SerializeField] private bool isPercentage;
    [SerializeField] private float healValue;

    public override void Use(Unit caster)
    {
        UseCost(caster);

        BattleSystem.Instance.UI.TextBox.SetText(magicText.GetLocalizedString(), () =>
        {
            foreach (var target in GetTargets())
            {
                float value = healValue;

                if (isPercentage)
                {
                    value = target.Character.HealthPoints.Value * healValue;
                }

                target.Heal(value * Effectiveness);
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
