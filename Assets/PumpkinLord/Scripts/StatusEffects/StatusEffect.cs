using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public abstract class StatusEffect
{
    [field: SerializeField] public Sprite Artwork { get; private set; }
    [field: SerializeField] public LocalizedString StatusName { get; private set; }
    [field: SerializeField] public LocalizedString StatusDescription { get; private set; }

    protected Unit unit;

    public virtual void OnAdd(Unit unit)
    {
        this.unit = unit;
    }

    public virtual void OnReAdd()
    {

    }

    public virtual void OnRemove(Unit unit) { }

    public virtual void OnBattleStart() { }
    public virtual void OnBattleEnd() { }
    public virtual void OnTurnStart() { }
    public virtual void OnTurnEnd() { }
}
