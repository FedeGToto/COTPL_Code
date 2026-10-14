using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UsableUI : Button
{
    [SerializeField] private UseListUI parent;

    [SerializeField] private Image artwork;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI quantityText;

    private ItemSO item;

    public void Setup(ItemSO item)
    {
        this.item = item;

        artwork.sprite = item.Icon;

        switch(item.Effect)
        {
            case MagicItem:
                MagicItem magic = item.Effect as MagicItem;
                // Set name
                itemName.text = magic.Spell.SpellName.GetLocalizedString();

                //Set quantity
                quantityText.text = magic.Spell.RealCost.ToString();

                if (magic.Spell.CostIsPercentage)
                    quantityText.text += "%";

                // Set interactability
                interactable = magic.Spell.CostCheck(BattleSystem.Instance.PlayerUnit);
                break;
            case ConsumableItem:
                ConsumableItem cons = item.Effect as ConsumableItem;
                // Set name
                itemName.text = item.Name;

                //Set quantity
                if (item.Stackable)
                    quantityText.text = GameManager.Instance.Player.Inventory.GetQuantity(item.ID).ToString();
                else
                    quantityText.text = "";

                // Set interactability
                if (!cons.CanBeUsed)
                    interactable = false;

                break;

        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        UpdateDescription();
    }
    
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        UpdateDescription();
    }

    public void UpdateDescription()
    {
        string description = "";

        switch(item.Effect)
        {
            case MagicItem:
                MagicItem magic = item.Effect as MagicItem;
                description = magic.Spell.SpellDescription.GetLocalizedString();
                break;
            case ConsumableItem:
                description = item.Description;
                break;
            default:
                description = "No description found for this item type";
                break;
        }

        parent.UpdateDescription(description);
    }

    public void Use()
    {
        parent.gameObject.SetActive(false);

        switch (item.Effect)
        {
            case MagicItem:
                MagicItem magic = item.Effect as MagicItem;
                magic.Spell.Use(BattleSystem.Instance.PlayerUnit);
                break;
            case ConsumableItem:
                ConsumableItem consumable = item.Effect as ConsumableItem;
                BattleSystem.Instance.UI.TextBox.SetText("Dullahan used an item.", () => // Change the text
                {
                    consumable.Use();

                    var playerTurn = BattleSystem.Instance.FSM.currentState as PlayerTurnState;
                    playerTurn.EndTurn(1.5f);
                });
                break;
            default:
                Debug.LogError("You should not see this message. An error has occurred");
                break;
        }
    }

}
