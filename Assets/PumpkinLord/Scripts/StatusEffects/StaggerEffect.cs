using DG.Tweening;
using UnityEngine;
using UnityEngine.Localization;

[System.Serializable]
public class PlayerStaggerEffect : StatusEffect
{
    [SerializeField] private LocalizedString staggerText;

    public override void OnRemove(Unit unit)
    {
        
    }

    public override void OnTurnEnd()
    {
        
    }

    public override void OnTurnStart()
    {
        BattleSystem battleSystem = unit.BattleSystem;
        battleSystem.UI.Bottom.SetOff();
        battleSystem.UI.TextBox.SetText(staggerText.GetLocalizedString()).OnComplete(() =>
        {
            var playerTurn = battleSystem.FSM.currentState as PlayerTurnState;
            playerTurn.EndTurn(1.5f);
            unit.Status.RemoveStatusEffect(this);
        });
    }
}

[System.Serializable]
public class EnemyStaggerEffect : StatusEffect
{
    [SerializeField] private LocalizedString staggerText;

    public override void OnRemove(Unit unit)
    {

    }

    public override void OnTurnEnd()
    {

    }

    public override void OnTurnStart()
    {
        BattleSystem battleSystem = unit.BattleSystem;
        battleSystem.UI.TextBox.SetText(staggerText.GetLocalizedString()).OnComplete(() =>
        {
            var enemyTurn = battleSystem.FSM.currentState as EnemyTurnState;
            enemyTurn.EndTurn(1.5f);
            unit.Status.RemoveStatusEffect(this);
        });
    }
}
