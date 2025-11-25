using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemButtonUI : Button
{
    [SerializeField] private InventoryUI parent;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI quantityText;

    private ItemSO item;

    public void Setup(ItemSO item, int quantity = 1)
    {
        this.item = item;
        itemIcon.sprite = item.Icon;

        if (item.Stackable)
        {
            quantityText.gameObject.SetActive(true);
            quantityText.text = quantity.ToString();
        }
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
