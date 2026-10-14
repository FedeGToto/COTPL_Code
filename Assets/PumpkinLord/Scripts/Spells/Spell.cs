using System;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Localization;

public abstract class Spell
{
    [field: SerializeField] public LocalizedString SpellName { get; private set; }
    [field: SerializeField] public LocalizedString SpellDescription { get; private set; }
    [field: SerializeField] public Target Target { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }
    public int RealCost => Mathf.RoundToInt(Cost * CostMultiplier);
    [field: SerializeField] public bool CostIsPercentage { get; private set; }

    [NonSerialized] public float Effectiveness = 1f;
    [NonSerialized] public float CostMultiplier = 1f;

    [Header("Text")]
    [SerializeField] protected LocalizedString magicText;

    public abstract void Use(Unit caster);
    public abstract bool CostCheck(Unit caster);
    public abstract void UseCost(Unit caster);

    public bool ManaCheck(Unit unit)
    {
        if (!CostIsPercentage && unit.Character.ManaPoints.Value < RealCost)
            return false;
        else if (CostIsPercentage && unit.Character.ManaPoints.Value <= 0)
            return false;
        else return true;
    }

    public void ManaUse(Unit unit)
    {
        int realCost = CostIsPercentage ?
            Mathf.RoundToInt(unit.Character.ManaPoints.Value * (Mathf.Min(100f, (float)RealCost) / 100f)) :
            RealCost;

        unit.Character.ManaPoints.Value -= realCost;
    }

    protected Unit[] GetTargets()
    {
        return Target switch
        {
            Target.Player => new Unit[] { BattleSystem.Instance.PlayerUnit },
            Target.Enemy => new Unit[] { BattleSystem.Instance.EnemyUnit },
            Target.All => new Unit[] { BattleSystem.Instance.PlayerUnit, BattleSystem.Instance.EnemyUnit },
            _ => null,
        };
    }
}
