using DG.Tweening;
using UnityEngine;

public class WinBattleState : State<BattleSystem>
{
    public override void EnterState(BattleSystem owner)
    {
        GameManager.Instance.Player.Souls += owner.EnemyUnit.GetComponentInChildren<Enemy>().Souls;
        GameManager.Instance.Player.Money += owner.EnemyUnit.GetComponentInChildren<Enemy>().Money;
        owner.UI.TextBox.SetText(owner.UI.WinText.GetLocalizedString()).OnComplete(() =>
        {
            GameManager.Instance.UnloadAdditiveScene(() =>
            {
                GameManager.Instance.Board.SetActive(true);
                EventManager.Instance.TriggerEvent(new BattleEndedEvent());
            });
        });
    }

    public override void ExitState(BattleSystem owner)
    {
        
    }

    public override void UpdateState(BattleSystem owner)
    {
        
    }
}
