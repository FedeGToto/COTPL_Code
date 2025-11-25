using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    [SerializeField] private Image artworkImage;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [Header("List")]
    [SerializeField] private EquipButtonUI buttonTemplate;
    [SerializeField] private Transform listParent;

    private void OnEnable()
    {
        DisplayItem(GameManager.Instance.Player.Inventory.Weapon);
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

        foreach (var item in inventory.GetEquipableItems())
        {
            var button = Instantiate(buttonTemplate, listParent);
            button.Setup(item);
        }

        buttonTemplate.gameObject.SetActive(false);
    }

    public void DisplayItem(ItemSO item)
    {
        artworkImage.sprite = item.Icon;

        titleText.text = item.Name;
        descriptionText.text = item.Description;
    }
}
