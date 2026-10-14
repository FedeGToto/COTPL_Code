using UnityEngine;

[System.Serializable]
public class FlameThrowerMagic : FireBallMagic
{
    [SerializeReference] private StatusEffect burnEffect;

    public override void Use(Unit caster)
    {
        base.Use(caster);

        foreach (var target in GetTargets())
        {
            target.Status.AddStatusEffect(burnEffect);
        }
    }
}
