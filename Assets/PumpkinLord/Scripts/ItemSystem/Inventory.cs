using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : SerializedMonoBehaviour
{
    [SerializeField] private Dictionary<ItemSO, int> startingConsumables;
    [SerializeField] private ItemSO startingWeapon;

    public ItemSO Weapon { get; private set; }

    private List<ItemSO> itemInstances = new();
    private Dictionary<string, int> items = new();

    private void Start()
    {
        foreach (var item in startingConsumables)
        {
            AddItem(item.Key, item.Value);
        }

        AddItem(startingWeapon);
        SetWeapon(GetItem(startingWeapon.ID));
    }

    public void AddItem(ItemSO item, int quantity = 1)
    {
        if (items.ContainsKey(item.ID))
        {
            if (item.Stackable)
            {
                items[item.ID] += quantity;
                EventManager.Instance.TriggerEvent(new ItemAddedEvent() { Item = item, Quantity = quantity });
            }
            else
            {
                Debug.LogWarning("Cannot stack a non-stackable item.");
                return;
            }
        }
        else
        {
            ItemSO itemInst = Instantiate(item);

            itemInstances.Add(itemInst);
            items.Add(itemInst.ID, quantity);
            itemInst.Effect.Parent = itemInst;

            EventManager.Instance.TriggerEvent(new ItemAddedEvent() { Item = item, Quantity = quantity });
        }
    }

    public void RemoveItem(string itemID, int quantity = 1)
    {
        if (items.ContainsKey(itemID))
        {
            items[itemID] -= quantity;
            if (items[itemID] <= 0)
            {
                var item = GetItem(itemID);
                if (item.Effect is ConsumableItem cons)
                    cons.BattleEnded();
                itemInstances.Remove(item);
            }
        }
    }

    public bool HasItem(string itemID, int quantity = 1)
    {
        ItemSO item = GetItem(itemID);

        if (item == null)
        {
            return false;
        }
        else
        {
            return items[itemID] >= quantity;
        }
    }

    public bool HasItem(ItemSO itemID, int quantity = 1)
    {
        return HasItem(itemID.ID, quantity);
    }

    public int GetQuantity(string id) => items[id];

    public void SetWeapon(ItemSO weapon)
    {
        EquippableItem effect;

        if (Weapon != null)
        {
            effect = Weapon.Effect as EquippableItem;
            effect.RemoveItem();
        }

        Weapon = weapon;

        effect = Weapon.Effect as EquippableItem;
        effect.EquipItem(weapon);

        EventManager.Instance.TriggerEvent(new ChangeEquipEvent() { Equip = weapon });
    }

    public ItemSO GetItem(string id)
    {
        ItemSO item = null;

        foreach (ItemSO itemInst in itemInstances)
        {
            if (itemInst.ID == id)
            {
                item = itemInst;
                break;
            }
        }

        return item;
    }

    public List<ItemSO> GetConsumableItems()
    {
        List<ItemSO> itemList = new List<ItemSO>();

        foreach (ItemSO item in itemInstances)
        {
            if (item.Effect is ConsumableItem)
            {
                itemList.Add(item);
            }
        }

        return itemList;
    }

    public List<ItemSO> GetMagicItems()
    {
        List<ItemSO> itemList = new List<ItemSO>();

        foreach (ItemSO item in itemInstances)
        {
            if (item.Effect is MagicItem)
            {
                itemList.Add(item);
            }
        }

        return itemList;
    }

    public List<ItemSO> GetEquipableItems()
    {
        List<ItemSO> itemList = new();

        foreach (ItemSO item in itemInstances)
        {
            if (item.Effect is EquippableItem)
            {
                itemList.Add(item);
            }
        }

        return itemList;
    }
}
