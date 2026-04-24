using System;
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

    public UnityEvent OnHeal;
    public UnityEvent OnTakeDamage;
    public UnityEvent OnDie;
    public UnityEvent OnAttack;

    public void PhysAttack(Unit target, float attackMultiplier = 1f, float defenseMultiplier = 1f)
    {
        OnAttack?.Invoke();

        float attack = Character.Stats["attack_phy"].Value * attackMultiplier;
        float defense = target.Character.Stats["defense_phy"].Value * defenseMultiplier;

        float damage = BattleHelper.CalculateDamage(attack, defense);

        BattleSystem.Instance.DealDamage(this, target, damage);
    }

    public void MagicAttack(Unit target, float attackMultiplier = 1f, float defenseMultiplier = 1f)
    {
        float attack = Character.Stats["attack_mag"].Value * attackMultiplier;
        float defense = target.Character.Stats["defense_mag"].Value * defenseMultiplier;

        float damage = BattleHelper.CalculateDamage(attack, defense);

        BattleSystem.Instance.DealDamage(this, target, damage);
    }

    public void TakeDamage(Unit source, float damage)
    {
        
        if (needsKillingBlow) {
            float currentHealth = Character.HealthPoints.Value;
            float projectedDamage = currentHealth -= damage;
            
            if (projectedDamage <= 0)
            {
                Character.HealthPoints.Value = 1;
                OnTakeDamage?.Invoke();
            }
            else
            {
                Character.HealthPoints.Value -= damage;
                OnTakeDamage?.Invoke();
            }

            if (Character.HealthPoints.Value <= 0)
                Kill();
        }
        else
        {
            Character.HealthPoints.Value -= damage;
            OnTakeDamage?.Invoke();

            if (Character.HealthPoints.Value <= 0)
                Kill();
        }
    }

    public void Heal(float healValue)
    {
        OnHeal?.Invoke();
        Character.HealthPoints.Value += healValue;
    }

    public void Kill()
    {
        OnDie?.Invoke();
    }
}
