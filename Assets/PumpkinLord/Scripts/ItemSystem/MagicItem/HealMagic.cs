using DG.Tweening;
using UnityEngine;
using UnityEngine.Localization;

[System.Serializable]
public class HealMagic : MagicItem
{
    [SerializeField] private bool isPercentage;
    [SerializeField] private float healValue;

    public override void Use()
    {
        Unit player = BattleSystem.Instance.PlayerUnit;
        float value = healValue;

        if (isPercentage)
        {
            value = player.Character.HealthPoints.Value * healValue;
        }

        player.Character.ManaPoints.Value -= Cost;

        BattleSystem.Instance.UI.TextBox.SetText(magicText.GetLocalizedString()).OnComplete(() =>
        {
            player.Heal(value);

            var playerTurn = BattleSystem.Instance.FSM.currentState as PlayerTurnState;
            playerTurn.EndTurn(1.5f);
        });
    }
}
