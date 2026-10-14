using StatSystem;
using UnityEngine;

public class InsanityEffect : StatusEffect
{
    public int StacksToAdd;

    [SerializeField] private int explodeStacks = 10;
    [SerializeField] private float explodeDamage = 0.35f;

    [SerializeField] private float buffAndDebuff = 0.001f;

    [SerializeReference] private StatusEffect stagger;

    public int Stacks
    {
        get => stacks;
        set
        {
            stacks = value;

            UpdateStat("attack_phy", buffAndDebuff);
            UpdateStat("attack_mag", buffAndDebuff);
            UpdateStat("defense_phy", -buffAndDebuff);
            UpdateStat("defense_mag", -buffAndDebuff);
        }
    }

    private int stacks;

    public override void OnAdd(Unit unit)
    {
        base.OnAdd(unit);
        Stacks += StacksToAdd;
    }

    public override void OnRemove(Unit unit)
    {
        base.OnRemove(unit);

        unit.Character.Stats["attack_phy"].TryRemoveAllModifiersOf(this);
        unit.Character.Stats["attack_mag"].TryRemoveAllModifiersOf(this);
        unit.Character.Stats["defense_phy"].TryRemoveAllModifiersOf(this);
        unit.Character.Stats["defense_mag"].TryRemoveAllModifiersOf(this);
    }

    public override void OnReAdd()
    {
        base.OnReAdd();

        if (unit.Status.GetAllStatuses().Find(x => x is InsanityEffect) is InsanityEffect existingEffect)
        {
            existingEffect.Stacks += StacksToAdd;
        }
    }

    public override void OnBattleEnd()
    {
        base.OnBattleEnd();
        Stacks = 0;
    }

    private void Explode()
    {
        DamageInfo info = new DamageInfo() {
            Source = unit,
            Target = unit,
            DamageType = DamageType.True,
            AttackType = AttackType.Spell,
            BaseDamage = explodeDamage * unit.Character.HealthPoints.MaxValue
        };
        unit.TakeDamage(info);
        unit.Status.RemoveStatusEffect(this);
        unit.Status.AddStatusEffect(stagger);

        Stacks = 0;
    }

    private void UpdateStat(string statId, float multiplier)
    {
        unit.Character.Stats[statId].TryRemoveAllModifiersOf(this);
        Modifier statModifier = new(stacks * multiplier, ModifierType.Multiplicative, this);
        unit.Character.Stats[statId].AddModifier(statModifier);
    }

    public override void OnTurnEnd()
    {
        base.OnTurnEnd();

        if (stacks >= explodeStacks)
            Explode();
    }

}
