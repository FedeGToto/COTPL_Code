using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    [SerializeField] private Unit unit;

    [Header("Bars")]
    [field: SerializeField] public ResourceBar HealthBar { get; private set; }
    [field: SerializeField] public ResourceBar ManaBar { get; private set; }

    [SerializeField] private Transform statusEffectsList;
    private List<StatusEffect> effects = new List<StatusEffect>();

    public void Setup()
    {
        HealthBar.SetupBar(unit.Character.HealthPoints);
        ManaBar.SetupBar(unit.Character.ManaPoints);
    }

    public void AddStatusEffect(StatusEffect statusEffect)
    {
        effects.Add(statusEffect);

        GameObject imageGO = new(statusEffect.StatusName.GetLocalizedString());
        imageGO.transform.SetParent(statusEffectsList);

        Image image = imageGO.AddComponent<Image>();
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 20);
        image.sprite = statusEffect.Artwork;
    }

    public void RemoveStatusEffect(StatusEffect statusEffect)
    {
        int index = effects.IndexOf(statusEffect);

        Destroy(statusEffectsList.GetChild(index).gameObject);

        effects.Remove(statusEffect);
    }
}
