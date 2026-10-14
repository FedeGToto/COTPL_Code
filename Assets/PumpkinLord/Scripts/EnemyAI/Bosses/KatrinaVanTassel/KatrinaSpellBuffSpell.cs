using UnityEngine;

public class KatrinaSpellBuffSpell : Spell
{
    [SerializeReference] private StatusEffect buffStat;
    [SerializeReference] private StatusEffect insanityStatus;

    public override bool CostCheck(Unit caster)
    {
        if (caster.Status.GetAllStatuses().Contains(buffStat))
            return false;

        return ManaCheck(caster);
    }

    public override void Use(Unit caster)
    {
        foreach (var target in GetTargets())
        {
            target.Status.AddStatusEffect(buffStat);
            target.Status.AddStatusEffect(insanityStatus);
        }
    }

    public override void UseCost(Unit caster)
    {
        ManaUse(caster);
    }
}
