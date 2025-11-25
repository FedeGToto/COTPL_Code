using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class EncyclopediaUI : MonoBehaviour, IMenuPage
{
    [SerializeField] private ItemSO[] items;

    [Header("Item List")]
    [SerializeField] private Button itemButton;
    [SerializeField] private Transform itemListParent;

    [Header("Item Descriptions")]
    [SerializeField] private Image itemArtwork;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI itemDescription2Text;

    public void OpenPage()
    {
        PopulateList();
    }

    public void ClosePage()
    {
        gameObject.SetActive(false);
    }

    private void PopulateList()
    {
        itemButton.gameObject.SetActive(true);
        for (int i = 0; i < itemListParent.childCount; i++)
        {
            if (i == 0) continue;

            Destroy(itemListParent.GetChild(i).gameObject);
        }

        foreach (var item in items)
        {
            Button buttonInstance = Instantiate(itemButton, itemListParent);
            buttonInstance.GetComponent<Image>().sprite = item.Icon;
            buttonInstance.onClick.AddListener(() => { ReadItem(item); });
        }

        itemButton.gameObject.SetActive(false);
        itemListParent.GetChild(1).GetComponent<Button>().onClick.Invoke();
    }

    private void ReadItem(ItemSO item)
    {
        itemArtwork.sprite = item.Icon;
        itemNameText.text = item.Name;
        itemDescriptionText.text = item.Description;
        itemDescription2Text.text = "";

        switch (item.Effect)
        {
            case MagicItem:
                MagicItem magicItem = item.Effect as MagicItem;
                itemDescription2Text.text = $"<style=title>Magic</style>" +
                    $"\r\n<b>{magicItem.SpellName.GetLocalizedString()}</b>" +
                    $"\r\nCost: {magicItem.Cost}{(magicItem.CostIsPercentage ? "%" : "")} MP" +
                    $"\r\n{magicItem.SpellDescription.GetLocalizedString()}";
                break;
        }
    }
}
