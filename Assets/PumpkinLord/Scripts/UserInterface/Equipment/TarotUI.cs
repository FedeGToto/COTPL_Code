using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TarotUI : MonoBehaviour
{
    [SerializeField] private TarotsManager tarotManager;

    [SerializeField] private Image artworkImage;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [Header("List")]
    [SerializeField] private TarotButtonUI buttonTemplate;
    [SerializeField] private Transform listParent;

    private void OnEnable()
    {
        DisplayItem(null, 0);
        SetupList();
    }

    public void SetupList()
    {
        for (int i = 0; i < listParent.childCount; i++)
        {
            if (i == 0) continue;

            var child = listParent.GetChild(i);
            Destroy(child.gameObject);
        }

        buttonTemplate.gameObject.SetActive(true);

        foreach (var item in tarotManager.GetTarotInventory())
        {
            var button = Instantiate(buttonTemplate, listParent);
            int level = item.Value;
            button.Setup(item.Key, level);
            DisplayItem(item.Key, item.Value);
        }

        buttonTemplate.gameObject.SetActive(false);
    }

    public void DisplayItem(TarotSO tarot, int level)
    {
        if (tarot == null)
        {
            artworkImage.enabled = false;
            titleText.text = "";
            descriptionText.text = "";
            return;
        }

        artworkImage.enabled = true;
        artworkImage.sprite = tarot.Icon;

        titleText.text = tarot.GetName();
        descriptionText.text = tarot.GetDescription(level - 1);
    }
}
