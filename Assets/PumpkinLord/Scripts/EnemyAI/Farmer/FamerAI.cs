using DG.Tweening;
using UnityEngine;
using UnityEngine.Localization;

public class FamerAI : EnemyAI
{
    [SerializeField] private float fleeChance = 0.1f;
    [SerializeField] private LocalizedString attackText;
    [SerializeField] private LocalizedString escapeText;

    public override void EvaluateNextAction()
    {
        int random = Random.Range(0, 100);

        if (random < 70)
        {
            // Do action 1
            Pitchfork();
        }
        else
        {
            // Do action 2
            Escape();
        }
    }

    public void Pitchfork()
    {
        BattleSystem battleSystem = Owner.Unit.BattleSystem;
        battleSystem.UI.TextBox.SetText(attackText.GetLocalizedString()).OnComplete(() =>
        {
            battleSystem.Attack(Owner.Unit, battleSystem.PlayerUnit);
            var enemyTurn = battleSystem.FSM.currentState as EnemyTurnState;
            enemyTurn.EndTurn(1.5f);
        });

    }

    public void Escape()
    {
        Owner.Unit.BattleSystem.UI.TextBox.SetText(escapeText.GetLocalizedString()).OnComplete(() =>
        {
            Owner.Unit.BattleSystem.Escape(Owner.Unit, fleeChance);
        });
    }
}
