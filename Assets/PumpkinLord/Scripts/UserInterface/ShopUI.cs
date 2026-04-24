using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.Rendering;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private ItemSO[] possibileItems;
    [SerializeField] private int itemsPerShop = 3;
    [SerializeField] private Vector2 randomDiscount;

    [Header("Item List")]
    [SerializeField] private ShopItemUI itemTemplate;
    [SerializeField] private Transform listTransform;

    [Header("Description")]
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;

    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI soulsText;

    [Header("Animation")]
    [SerializeField] private RectTransform shopRect;
    [SerializeField] private float openPos = -32f;
    [SerializeField] private float closePos = 1000f;
    [SerializeField] private float animationDuration;

    public void OpenShop()
    {
        GameManager.Instance.Input.ChangeContext(InputManager.InputContext.UserInterface);
        moneyText.text = GameManager.Instance.Player.Money.ToString();
        soulsText.text = GameManager.Instance.Player.Souls.ToString();

        var items = GetItems();

        for (int i = 0; i < listTransform.childCount; i++)
        {
            if (i == 0) continue;
            Destroy(listTransform.GetChild(i).gameObject);
        }

        itemTemplate.gameObject.SetActive(true);

        for (int i = 0; i < items.Count; i++)
        {
            var button = Instantiate(itemTemplate, listTransform);

            float discount = i == 0 ? Random.Range(randomDiscount.x, randomDiscount.y) : 0;
            button.Setup(items[i], discount);

            if (i == 0)
            {
                button.Select();
                button.UpdateText();
            }
        }

        itemTemplate.gameObject.SetActive(false);

        shopRect.anchoredPosition = new Vector2(closePos, shopRect.anchoredPosition.y);
        gameObject.SetActive(true);
        shopRect.DOAnchorPosX(openPos, animationDuration).OnComplete(()=>
        {

        });
    }

    public void CloseShop()
    {
        shopRect.anchoredPosition = new Vector2(openPos, shopRect.anchoredPosition.y);
        shopRect.DOAnchorPosX(closePos, animationDuration).OnComplete(() =>
        {
            GameManager.Instance.Input.SetOldContext();
            gameObject.SetActive(false);
            EventManager.Instance.TriggerEvent(new CloseShopEvent());
        });
    }

    public void SetDescription(string title, string description)
    {
        itemName.text = title;
        itemDescription.text = description;
    }

    private List<ItemSO> GetItems()
    {
        List<ItemSO> extractedItems = new();

        for (int i = 0; i < itemsPerShop; i++)
        {
            ItemSO item;
            do
            {
                item = possibileItems[Random.Range(0, possibileItems.Length)];
            } while (extractedItems.Contains(item));
            extractedItems.Add(item);
        }

        return extractedItems;
    }
}
