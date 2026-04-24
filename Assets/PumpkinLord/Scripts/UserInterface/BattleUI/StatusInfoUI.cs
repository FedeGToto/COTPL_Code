using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusInfoUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI statusName;
    [SerializeField] private TextMeshProUGUI statusDescription;

    public void Init(StatusEffect effect)
    {
        iconImage.sprite = effect.Artwork;
        statusName.text = effect.StatusName.GetLocalizedString();
        statusDescription.text = effect.StatusDescription.GetLocalizedString();
    }
}
