using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Image artworkImage;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [Header("List")]
    [SerializeField] private ItemButtonUI buttonTemplate;
    [SerializeField] private Transform listParent;

    private void OnEnable()
    {
        DisplayItem(null);
        SetupList();
    }

    public void SetupList()
    {
        var inventory = GameManager.Instance.Player.Inventory;

        for (int i = 0; i < listParent.childCount; i++)
        {
            if (i == 0) continue;

            var child = listParent.GetChild(i);
            Destroy(child.gameObject);
        }

        buttonTemplate.gameObject.SetActive(true);

        foreach (var item in inventory.GetConsumableItems())
        {
            var button = Instantiate(buttonTemplate, listParent);
            int quantity = inventory.GetQuantity(item.ID);
            button.Setup(item, quantity);
            DisplayItem(item);
        }

        foreach (var item in inventory.GetMagicItems())
        {
            var button = Instantiate(buttonTemplate, listParent);
            button.Setup(item);
        }

        buttonTemplate.gameObject.SetActive(false);
    }

    public void DisplayItem(ItemSO item)
    {
        if (item == null)
        {
            artworkImage.enabled = false;
            titleText.text = "";
            descriptionText.text = "";
            return;
        }

        artworkImage.enabled = true;
        artworkImage.sprite = item.Icon;

        titleText.text = item.Name;
        descriptionText.text = item.Description;
    }
}
