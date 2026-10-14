using DG.Tweening;
using UnityEngine;

public class UnitTurnState : State<BattleSystem>
{
    protected Timer NextTurnTimer;
    protected BattleSystem Owner;

    public override void EnterState(BattleSystem owner)
    {
        this.Owner = owner;
    }

    public override void ExitState(BattleSystem owner)
    {
        
    }

    public override void UpdateState(BattleSystem owner)
    {
        NextTurnTimer?.Update();
    }

    public virtual void EndTurn(float endTurnDuration)
    {
        NextTurnTimer = new Timer(endTurnDuration);
        NextTurnTimer.Start();
    }
}
