using DG.Tweening;
using Rewired;
using UnityEngine;

[System.Serializable]
public class HeadThrowMagic : MagicItem
{
    [SerializeField] private string itemNeeded;
    [SerializeField] private float attackMultiplier = 2f;

    public override void Use()
    {
        Unit player = BattleSystem.Instance.PlayerUnit;
        Unit enemy = BattleSystem.Instance.EnemyUnit;

        if (GameManager.Instance.Player.Inventory.HasItem(itemNeeded))
        {
            GameManager.Instance.Player.Inventory.RemoveItem(itemNeeded, 1);
        }
        else
        {
            float manaCost = player.Character.ManaPoints.Value * Cost;

            player.Character.ManaPoints.Value -= manaCost;
        }

        BattleSystem.Instance.UI.TextBox.SetText(magicText.GetLocalizedString()).OnComplete(() =>
        {
            // Check if is boss
            if (BattleSystem.Instance.EnemyUnit.Tags.HasTag("Boss"))
            {
                float attack = player.Character.Stats["attack_mag"].Value * attackMultiplier;
                float defense = enemy.Character.Stats["defense_mag"].Value;

                BattleSystem.Instance.Magic(player, enemy, attack, defense);

                var playerTurn = BattleSystem.Instance.FSM.currentState as PlayerTurnState;
                playerTurn.EndTurn(1.5f);
            }
            else
            {
                enemy.Kill();
            }
        });
    }
}
