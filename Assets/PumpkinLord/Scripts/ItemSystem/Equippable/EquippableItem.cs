using UnityEngine;

public abstract class EquippableItem : ItemEffect
{
    public abstract void EquipItem(ItemSO parent);
    public abstract void RemoveItem();
}
