using UnityEngine;

[System.Serializable]
public class BurnEffect : StatusEffect
{
    [SerializeField] private int duration;
    [SerializeField] private float damagePerTurn;

    private int currentDuration;

    public override void OnAdd(Unit unit)
    {
        base.OnAdd(unit);
        currentDuration = duration;
    }

    public override void OnTurnEnd()
    {
        currentDuration--;

        if (currentDuration <= 0)
        {
            unit.Status.RemoveStatusEffect(this);
        }    
    }

    public override void OnTurnStart()
    {
        DamageInfo info = new()
        {
            Target = unit,
            Source = unit,
            DamageType = DamageType.Magical,
            BaseDamage = damagePerTurn
        };

        unit.TakeDamage(info);
    }
}
