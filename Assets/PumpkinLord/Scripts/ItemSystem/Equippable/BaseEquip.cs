using StatSystem;
using UnityEngine;

[System.Serializable]
public class BaseEquip : EquippableItem
{
    [SerializeField] private string stat = "attack_phy";
    [SerializeField] private int statBuff = 1;

    Modifier statModifier;

    public override void EquipItem(ItemSO parent)
    {
        this.Parent = parent;
        statModifier = new(statBuff, ModifierType.Flat, Parent);
        GameManager.Instance.Player.Character.Stats[stat].AddModifier(statModifier);
    }

    public override void RemoveItem()
    {
        GameManager.Instance.Player.Character.Stats[stat].TryRemoveModifier(statModifier);
    }
}
