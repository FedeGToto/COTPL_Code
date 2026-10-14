using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TarotButtonUI : Button
{
    [SerializeField] private TarotUI parent;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI levelText;

    private TarotSO tarot;
    private int level;

    public void Setup(TarotSO tarot, int level)
    {
        this.tarot = tarot;
        this.level = level;

        itemIcon.sprite = tarot.Icon;
        levelText.text = "LV. " + level;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        parent.DisplayItem(tarot, level);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);

        parent.DisplayItem(tarot, level);
    }
}
