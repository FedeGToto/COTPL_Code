using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class FireBallMagic : MagicItem
{
    [SerializeField] private float attackMultiplier = 1f;
    [SerializeField] private float defenseMultiplier = 1f;

    public override void Use()
    {
        Unit player = BattleSystem.Instance.PlayerUnit;
        Unit enemy = BattleSystem.Instance.EnemyUnit;

        float attack = player.Character.Stats["attack_mag"].Value * attackMultiplier;
        float defense = enemy.Character.Stats["defense_mag"].Value * defenseMultiplier;

        player.Character.ManaPoints.Value -= Cost;

        BattleSystem.Instance.UI.TextBox.SetText(magicText.GetLocalizedString()).OnComplete(() =>
        {
            BattleSystem.Instance.Magic(player, enemy, attack, defense);

            var playerTurn = BattleSystem.Instance.FSM.currentState as PlayerTurnState;
            playerTurn.EndTurn(1.5f);
        });
    }
}
