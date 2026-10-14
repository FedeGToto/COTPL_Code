using DG.Tweening;
using UnityEngine;
using UnityEngine.Localization;

[System.Serializable]
public class PlayerStaggerEffect : StatusEffect
{
    [SerializeField] private LocalizedString staggerText;

    public override void OnTurnStart()
    {
        BattleSystem battleSystem = unit.BattleSystem;
        battleSystem.UI.Bottom.SetOff();
        battleSystem.UI.TextBox.SetText(staggerText.GetLocalizedString(), () =>
        {
            var turn = battleSystem.FSM.currentState as UnitTurnState;
            turn.EndTurn(1.5f);
            unit.Status.RemoveStatusEffect(this);
        });
    }
}
