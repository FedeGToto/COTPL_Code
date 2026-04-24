using DG.Tweening;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.UI;

public class PlayerActionsUI : MonoBehaviour
{
    [SerializeField] private BattleSystem battle;
    [SerializeField] private BottomUI parent;

    [Header("UI")]
    [SerializeField] private Button[] buttons;
    [SerializeField] private LocalizedString[] attackString;
    [SerializeField] private LocalizedString[] fleeString;

    private void Start()
    {
        DisableButton();
    }

    public void EnableButtons()
    {
        foreach (var button in buttons)
        {
            button.interactable = true;

            var attackString = buttons[0].GetComponentInChildren<LocalizeStringEvent>();
            FloatVariable healthValue = (attackString.StringReference["enemyHealth"]) as FloatVariable;
            healthValue.Value = battle.EnemyUnit.Character.HealthPoints.Value;
            attackString.RefreshString();
        }

        buttons[0].Select();
    }

    public void DisableButton()
    {
        foreach (var button in buttons)
        {
            button.interactable = false;
        }
    }

    public void Attack()
    {
        DisableButton();
        LocalizedString selectedAttackString = attackString[Random.Range(0, attackString.Length)];
        battle.UI.TextBox.SetText(selectedAttackString.GetLocalizedString()).OnComplete(() =>
        {
            float enemyHealth = battle.EnemyUnit.Character.HealthPoints.Value;
            if (enemyHealth > 1)
            {
                battle.PlayerUnit.PhysAttack(battle.EnemyUnit);

                var playerTurn = battle.FSM.currentState as PlayerTurnState;
                playerTurn.EndTurn(1.5f);
            }
            else
                battle.EnemyUnit.Kill();
        });
        
    }

    public void Magic()
    {
        PlayerManager player = GameManager.Instance.Player;
        var magicItems = player.Inventory.GetMagicItems();

        if (magicItems.Count > 0)
        {
            DisableButton();
            parent.UseList.OpenList(player.Inventory.GetMagicItems());
        }
    }

    public void Items()
    {
        PlayerManager player = GameManager.Instance.Player;
        var consumableItems = player.Inventory.GetConsumableItems();

        if (consumableItems.Count > 0)
        {
            DisableButton();
            parent.UseList.OpenList(player.Inventory.GetConsumableItems());
        }
    }

    public void Escape()
    {
        DisableButton();
        LocalizedString selectedFleeString = fleeString[Random.Range(0, fleeString.Length)];
        battle.UI.TextBox.SetText(selectedFleeString.GetLocalizedString()).OnComplete(() =>
        {
            battle.Escape(battle.PlayerUnit, 0.6f);
        });
    }
}
