using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class FireBallMagic : Spell
{
    [SerializeField] private float attackMultiplier = 1f;
    [SerializeField] private float defenseMultiplier = 1f;

    public override void Use(Unit caster)
    {
        UseCost(caster);

        caster.OnSpellUsed?.Invoke(this);

        BattleSystem.Instance.UI.TextBox.SetText(magicText.GetLocalizedString(), () =>
        {
            foreach (var target in GetTargets())
            {
                caster.MagicAttack(target, attackMultiplier * Effectiveness, defenseMultiplier);
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
