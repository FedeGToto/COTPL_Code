using System;
using UnityEngine;

public class PlayerTurnState : State<BattleSystem>
{
    private Timer nextTurnTimer;
    private BattleSystem owner;

    public override void EnterState(BattleSystem owner)
    {
        this.owner = owner;

        owner.UI.Bottom.SetOn();

        owner.PlayerUnit.Status.CheckStartTurn();
    }

    public override void ExitState(BattleSystem owner)
    {
        owner.PlayerUnit.Status.CheckEndTurn();

        owner.UI.Bottom.SetOff();
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
            owner.FSM.ChangeState(new EnemyTurnState());
        };
    }
}
