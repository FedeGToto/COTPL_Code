using UnityEngine;
using UnityEngine.Localization;

public class PlayerInfoGenerator : MonoBehaviour
{
    [SerializeField] private LocalizedString attackText;

    private Unit playerUnit;

    private void Start()
    {
        playerUnit = GetComponent<Unit>();

        string text = GenerateText();

        playerUnit.AttacksDescription = text;
    }

    private string GenerateText()
    {
        string result = "";
        PlayerManager player = GameManager.Instance.Player;

        result += attackText.GetLocalizedString();

        var magicItems = player.Inventory.GetMagicItems();

        if (magicItems.Count > 0)
        {
            foreach (var item in magicItems)
            {
                GenerateItemDescription(ref result, item);
            }
        }

        return result;
    }

    private void GenerateItemDescription(ref string result, ItemSO item)
    {
        var magic = item.Effect as MagicItem;

        string spellName = magic.Spell.SpellName.GetLocalizedString();
        string spellCost = magic.Spell.RealCost.ToString();
        if (magic.Spell.CostIsPercentage)
            spellCost += "%";
        spellCost += " MP. ";
        string spellDescription = magic.Spell.SpellDescription.GetLocalizedString();

        result += "\n\n<b>" + spellName + "</b> - " + spellCost + spellDescription;
    }
}
