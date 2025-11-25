using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public abstract class StatusEffect
{
    [field: SerializeField] public Sprite Artwork { get; private set; }
    [field: SerializeField] public LocalizedString StatusName { get; private set; }

    protected Unit unit;

    public virtual void OnAdd(Unit unit)
    {
        this.unit = unit;
    }

    public abstract void OnRemove(Unit unit);
    public abstract void OnTurnStart();
    public abstract void OnTurnEnd();
}
