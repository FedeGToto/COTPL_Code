using UnityEngine;

public abstract class ConsumableItem : ItemEffect
{
    [field: SerializeField] public bool CanBeUsed { get; private set; }

    public virtual void BattleStart() { }
    public virtual void BattleEnded() { }

    public virtual void Use()
    {
        GameManager.Instance.Player.Inventory.RemoveItem(Parent.ID, 1);
    }
}
