using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "New Item", menuName = "Pumpkin Lord/Item")]
public class ItemSO : ScriptableObject
{
    [field: SerializeField] public string ID { get; private set; }
    [field: SerializeField] public bool Stackable { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }

    [SerializeField] private LocalizedString itemName;
    [SerializeField] private LocalizedString itemDescription;
    [SerializeField] private Sprite icon;
    [SerializeReference] private ItemEffect effect;

    public string Name => itemName.GetLocalizedString();
    public string Description => itemDescription.GetLocalizedString();
    public Sprite Icon => icon;
    public ItemEffect Effect => effect;
}
