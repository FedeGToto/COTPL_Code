using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemUI : Button
{
    [SerializeField] private ShopUI parent;

    [SerializeField] private string discountFormat = "<s><color={0}>{1}</color></s>\n{2}";
    [SerializeField] private Image artwork;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private Color discountColor = Color.gray;
    [SerializeField] private GameObject discountIcon;
    [SerializeField] private TextMeshProUGUI discountText;

    private ItemSO item;
    private float discount;

    public void Setup(ItemSO item, float discount)
    {
        this.item = item;
        this.discount = discount;

        float price = item.Cost;

        artwork.sprite = item.Icon;
        cost.text = price.ToString();

        if (discount > 0)
        {
            //Open the discount image
            cost.text = string.Format(discountFormat, ColorUtility.ToHtmlStringRGBA(discountColor), price, Mathf.RoundToInt(price - (price * discount)));

            discountText.text = $"-{(discount * 100):00}%";
            discountIcon.SetActive(true);
        }
    }

    public void Buy()
    {
        var player = GameManager.Instance.Player;
        int realPrice = Mathf.RoundToInt(discount > 0 ? item.Cost * discount : item.Cost);

        if (player.Money < realPrice)
            return;

        if (!item.Stackable && player.Inventory.HasItem(item))
            return;

        Debug.Log("Acquired Item");
        player.Money -= realPrice;
        player.Inventory.AddItem(item, 1);

        parent.CloseShop();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        UpdateText();
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);

        UpdateText();
    }

    public void UpdateText()
    {
        string title = item.Name;
        string description = item.Description;

        parent.SetDescription(title, description);
    }
}
