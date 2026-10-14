using UnityEngine;

public class DamageInfo
{
    public Unit Source;
    public Unit Target;
    public DamageType DamageType;
    public AttackType AttackType;
    public float BaseDamage;
    public float Multiplier = 1f;

    public float FinalDamage => Mathf.Max(0f, BaseDamage * Multiplier);
}
