[System.Serializable]
public class AttackBoostEffect : StatusEffect
{
    public float BoostMultiplier;

    public override void OnAdd(Unit unit)
    {
        base.OnAdd(unit);

        unit.OnDamageDealt.AddListener(ApplyBoost);
    }

    private void ApplyBoost(DamageInfo info)
    {
        info.Multiplier = 1 + BoostMultiplier;
    }

    public override void OnRemove(Unit unit)
    {
        unit.OnDamageDealt.RemoveListener(ApplyBoost);
    }
}
