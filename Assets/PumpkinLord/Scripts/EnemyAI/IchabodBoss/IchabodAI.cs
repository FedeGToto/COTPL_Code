using UnityEngine;

[System.Serializable]
public class IchabodAI : EnemyAI
{
    public override void EvaluateNextAction()
    {
        BattleSystem battleSystem = Owner.Unit.BattleSystem;
        var enemyTurn = battleSystem.FSM.currentState as EnemyTurnState;
        enemyTurn.EndTurn(1.5f);
    }
}
