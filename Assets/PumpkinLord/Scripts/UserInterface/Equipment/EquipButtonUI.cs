using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipButtonUI : Button
{
    [SerializeField] private EquipmentUI parent;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image selectedIcon;

    private ItemSO item;

    public void Setup(ItemSO item)
    {
        EventManager.Instance.AddListener<ChangeEquipEvent>(OnChangeEquip);

        this.item = item;
        itemIcon.sprite = item.Icon;

        selectedIcon.gameObject.SetActive(false);

        if (GameManager.Instance.Player.Inventory.Weapon.ID == item.ID)
            selectedIcon.gameObject.SetActive(true);
    }

    protected override void OnDestroy()
    {
        EventManager.Instance.RemoveListener<ChangeEquipEvent>(OnChangeEquip);
    }

    private void OnChangeEquip(ChangeEquipEvent e)
    {
        selectedIcon.gameObject.SetActive(e.Equip.ID == item.ID);
    }

    public void Equip()
    {
        var inventory = GameManager.Instance.Player.Inventory;
        inventory.SetWeapon(inventory.GetItem(item.ID));

        parent.DisplayItem(item);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        parent.DisplayItem(item);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);

        parent.DisplayItem(item);
    }
}
