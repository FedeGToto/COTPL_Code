using UnityEngine;
using UnityEngine.Localization;

public abstract class MagicItem : ItemEffect
{
    [field: SerializeField] public LocalizedString SpellName { get; private set; }
    [field: SerializeField] public LocalizedString SpellDescription { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }
    [field: SerializeField] public bool CostIsPercentage { get; private set; }

    [Header("Text")]
    [SerializeField] protected LocalizedString magicText;

    public abstract void Use();
}
