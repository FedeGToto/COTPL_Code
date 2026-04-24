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

    public List<StatusEffect> GetAllStatuses() => statusEffects;

    public void CheckStartTurn()
    {
        for (int i = statusEffects.Count - 1; i >= 0; i--)
        {
            statusEffects[i].OnTurnStart();
        }
    }

    public void CheckEndTurn()
    {
        for (int i = statusEffects.Count - 1; i >= 0; i--)
        {
            statusEffects[i].OnTurnEnd();
        }
    }

    public void CheckDealDamage()
    {

    }
}
