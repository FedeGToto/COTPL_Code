using StatSystem;
using UnityEngine;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

[SerializeField]
public class StatBoostEffect : StatusEffect
{
    [SerializeField] private Target target;
    [SerializeField] private string statID;
    [field: SerializeField] public float Value;

    public override void OnAdd(Unit unit)
    {
        if (StatusDescription.TryGetValue("value", out var variable))
        {
            (variable as FloatVariable).Value = Value * 100f;
        }

        base.OnAdd(unit);

        var targets = GetTargets();

        foreach (var target in targets)
        {
            Modifier modifier = new(Value, ModifierType.Flat, this);
            target.Character.Stats[statID].AddModifier(modifier);
        }
    }

    public override void OnRemove(Unit unit)
    {
        var targets = GetTargets();

        foreach (var target in targets)
        {
            target.Character.Stats[statID].TryRemoveAllModifiersOf(this);
        }
    }

    private Unit[] GetTargets()
    {
        return target switch
        {
            Target.Player => new Unit[] { BattleSystem.Instance.PlayerUnit },
            Target.Enemy => new Unit[] { BattleSystem.Instance.EnemyUnit },
            Target.All => new Unit[] { BattleSystem.Instance.PlayerUnit, BattleSystem.Instance.EnemyUnit },
            _ => null,
        };
    }
}
