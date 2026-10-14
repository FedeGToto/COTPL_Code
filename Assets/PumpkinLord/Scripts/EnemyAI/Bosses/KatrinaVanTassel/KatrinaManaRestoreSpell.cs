using UnityEngine;

public class KatrinaManaRestoreSpell : ManaRestoreSpell
{
    [SerializeReference] private StatusEffect insanityEffect;

    public override void Use(Unit caster)
    {
        foreach (var target in GetTargets())
        {
            target.Status.AddStatusEffect(insanityEffect);
        }

        base.Use(caster);
    }
}
