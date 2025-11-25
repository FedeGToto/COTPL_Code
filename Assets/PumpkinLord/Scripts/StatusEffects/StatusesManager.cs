using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatusesManager : MonoBehaviour
{
    private List<StatusEffect> statusEffects = new List<StatusEffect>();
    private Unit unit;

    public UnityEvent<StatusEffect> OnStatusAdded;
    public UnityEvent<StatusEffect> OnStatusRemoved;

    private void Start()
    {
        unit = GetComponent<Unit>();
    }

    public void AddStatusEffect(StatusEffect effect)
    {
        if (statusEffects.Contains(effect)) return;

        statusEffects.Add(effect);
        effect.OnAdd(unit);

        OnStatusAdded?.Invoke(effect);
    }

    public void RemoveStatusEffect(StatusEffect effect)
    {
        effect.OnRemove(unit);
        statusEffects.Remove(effect);

        OnStatusRemoved?.Invoke(effect);
    }

    public void CheckStartTurn()
    {
        foreach (var effect in statusEffects)
        {
            effect.OnTurnStart();
        }
    }

    public void CheckEndTurn()
    {
        foreach (var effect in statusEffects)
        {
            effect.OnTurnEnd();
        }
    }

    public void CheckDealDamage()
    {

    }
}
