using DG.Tweening;
using UnityEngine;

public class StartBattleState : State<BattleSystem>
{
    private Timer stateTimer;

    public override void EnterState(BattleSystem owner)
    {
        // Setup units
        owner.EnemyUnit.Character.SetupStats();
        owner.PlayerUnit.Character = GameManager.Instance.Player.Character;

        owner.UI.StartBattle();
        var textTween = owner.UI.TextBox.SetText(owner.UI.PickStarterText(owner.EnemyUnit.UnitName));

        stateTimer = new Timer(textTween.Duration() + 3f);
        stateTimer.OnTimerStop += () =>
        {
            owner.FSM.ChangeState(new PlayerTurnState());
        };

        stateTimer.Start();
    }

    public override void ExitState(BattleSystem owner)
    {
        owner.UI.TextBox.SetText("");
    }

    public override void UpdateState(BattleSystem owner)
    {
        stateTimer.Update();
    }
}
