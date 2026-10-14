using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    [field: SerializeField] public BattleSystem BattleSystem { get; private set; }
    [field: SerializeField] public TagsManager Tags { get; private set; }
    [field: SerializeField] public StatusesManager Status { get; private set; }

    public Character Character;

    [SerializeField] private bool needsKillingBlow;
    public string UnitName { get; set; }
    public string AttacksDescription { get; set; }

    readonly List<IDeathSaver> savers = new();

    public UnityEvent OnHeal;
    public UnityEvent<DamageInfo> OnDamageDealt;
    public UnityEvent<DamageInfo> OnBeforeDamage;
    public UnityEvent OnTakeDamage;
    public UnityEvent<Spell> OnSpellUsed;
    public UnityEvent OnDie;
    public UnityEvent OnAttack;

    public void PhysAttack(Unit target, float attackMultiplier = 1f, float defenseMultiplier = 1f, AttackType type = AttackType.BaseAttack)
    {
        OnAttack?.Invoke();

        float attack = Character.Stats["attack_phy"].Value * attackMultiplier;
        float defense = target.Character.Stats["defense_phy"].Value * defenseMultiplier;

        float damage = BattleHelper.CalculateDamage(attack, defense);

        DamageInfo info = new() { Source = this, Target = target, DamageType = DamageType.Physical, AttackType = type, BaseDamage = damage };

        BattleSystem.Instance.DealDamage(info);
    }

    public void MagicAttack(Unit target, float attackMultiplier = 1f, float defenseMultiplier = 1f, AttackType type = AttackType.Spell)
    {
        float attack = Character.Stats["attack_mag"].Value * attackMultiplier;
        float defense = target.Character.Stats["defense_mag"].Value * defenseMultiplier;

        float damage = BattleHelper.CalculateDamage(attack, defense);

        DamageInfo info = new() { Source = this, Target = target, DamageType = DamageType.Magical, AttackType = type, BaseDamage = damage };

        BattleSystem.Instance.DealDamage(info);
    }


    public void TakeDamage(DamageInfo damageInfo)
    {
        damageInfo.Source?.OnDamageDealt?.Invoke(damageInfo);
        OnBeforeDamage?.Invoke(damageInfo);

        float projectedHealth = Character.HealthPoints.Value - damageInfo.FinalDamage;
        bool lethal = projectedHealth <= 0f;

        Character.HealthPoints.Value = (lethal && needsKillingBlow) ? 1 : projectedHealth;
        OnTakeDamage?.Invoke();

        if (Character.HealthPoints.Value <= 0)
            Kill();
    }

    public void Heal(float healValue)
    {
        OnHeal?.Invoke();
        Character.HealthPoints.Value += healValue;
    }

    public void Kill()
    {
        foreach (var saver in savers.OrderByDescending(s => s.Priority))
        {
            if (saver.TrySave(Character.HealthPoints)) return;
        }

        OnDie?.Invoke();
    }

    public void RegisterDeathSave(IDeathSaver deathSaver) => savers.Add(deathSaver);
    public void UnregisterDeathSave(IDeathSaver deathSaver) => savers.Remove(deathSaver);
}
