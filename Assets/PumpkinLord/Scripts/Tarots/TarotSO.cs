using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "New Tarot", menuName = "Pumpkin Lord/Tarot")]
public class TarotSO : ScriptableObject
{
    [field: SerializeField] public string ID { get; private set; }
    [SerializeField] private LocalizedString tarotName;
    [SerializeField] private LocalizedString tarotDescription;
    [SerializeField] private Sprite icon;

    [SerializeReference] private TarotEffect effect;

    public TarotEffect Effect => effect;

    public string GetName() => tarotName.GetLocalizedString();
    public string GetDescription(int level) => effect.GetDescription(tarotDescription, level);
    public Sprite Icon => icon;
}
