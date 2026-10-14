using UnityEngine;

public class InsanityGiveSpell : Spell
{
    [SerializeReference] private StatusEffect insanityStatus;

    public override bool CostCheck(Unit caster)
    {
        return ManaCheck(caster);
    }

    public override void Use(Unit caster)
    {
         foreach (var target in GetTargets())
            target.Status.AddStatusEffect(insanityStatus);
    }

    public override void UseCost(Unit caster)
    {
        ManaUse(caster);
    }
}
