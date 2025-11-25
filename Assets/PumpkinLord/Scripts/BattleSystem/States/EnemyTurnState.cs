using DG.Tweening;
using UnityEngine;

public class EnemyTurnState : State<BattleSystem>
{
    private Timer nextTurnTimer;
    private BattleSystem owner;

    public override void EnterState(BattleSystem owner)
    {
        this.owner = owner;

        owner.EnemyUnit.Status.CheckStartTurn();

        owner.UI.TextBox.SetText(owner.UI.EnemyTurnText.GetLocalizedString()).OnComplete(() =>
        {
            owner.EnemyUnit.GetComponentInChildren<Enemy>().AI.EvaluateNextAction();
        });
    }

    public override void ExitState(BattleSystem owner)
    {
        owner.EnemyUnit.Status.CheckEndTurn();
    }

    public override void UpdateState(BattleSystem owner)
    {
        nextTurnTimer?.Update();
    }

    public void EndTurn(float endTurnDuration)
    {
        nextTurnTimer = new Timer(endTurnDuration);
        nextTurnTimer.Start();
        nextTurnTimer.OnTimerStop += () =>
        {
            owner.FSM.ChangeState(new PlayerTurnState());
        };
    }
}
