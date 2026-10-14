using TMPro;
using UnityEngine;

public class TarotSelectUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cardName;
    [SerializeField] TextMeshProUGUI cardDescription;

    private TarotSO tarot;
    private TarotsManager tarotsManager;

    public void UpdateCard(TarotsManager tarotsManager, TarotSO tarot)
    {
        this.tarotsManager = tarotsManager;
        this.tarot = tarot;

        cardName.text = tarot.GetName();
        cardDescription.text = tarot.GetDescription(tarotsManager.GetLevel(tarot));
    }

    public void Select()
    {
        tarotsManager.PickTarot(tarot);
    }
}
