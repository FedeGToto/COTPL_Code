using System;
using UnityEngine;

public class PlayerTurnState : UnitTurnState
{
    public override void EnterState(BattleSystem owner)
    {
        base.EnterState(owner);

        owner.UI.Bottom.SetOn();

        owner.PlayerUnit.Status.CheckStartTurn();
        
        if (TarotsManager.Instance != null)
        {
            TarotsManager.Instance.CheckStartTurn();
        }
    }

    public override void ExitState(BattleSystem owner)
    {
        owner.PlayerUnit.Status.CheckEndTurn();

        if (TarotsManager.Instance != null)
        {
            TarotsManager.Instance.CheckEndTurn();
        }

        owner.UI.Bottom.SetOff();
    }

    public override void EndTurn(float endTurnDuration)
    {
        base.EndTurn(endTurnDuration);

        NextTurnTimer.OnTimerStop += () =>
        {
            Owner.FSM.ChangeState(new EnemyTurnState());
        };
    }
}
