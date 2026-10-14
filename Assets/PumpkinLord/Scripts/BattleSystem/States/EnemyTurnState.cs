using DG.Tweening;
using UnityEngine;

public class EnemyTurnState : UnitTurnState
{
    public override void EnterState(BattleSystem owner)
    {
        base.EnterState(owner);

        owner.EnemyUnit.Status.CheckStartTurn();

        owner.UI.TextBox.SetText(owner.UI.EnemyTurnText.GetLocalizedString(), () =>
        {
            owner.EnemyUnit.GetComponentInChildren<Enemy>().AI.EvaluateNextAction();
        });
    }

    public override void ExitState(BattleSystem owner)
    {
        owner.EnemyUnit.Status.CheckEndTurn();
    }

    public override void EndTurn(float endTurnDuration)
    {
        base.EndTurn(endTurnDuration);

        NextTurnTimer.OnTimerStop += () =>
        {
            Owner.FSM.ChangeState(new PlayerTurnState());
        };
    }
}
