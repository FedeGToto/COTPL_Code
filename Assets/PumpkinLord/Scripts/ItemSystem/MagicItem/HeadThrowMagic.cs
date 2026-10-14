using DG.Tweening;
using Rewired;
using StatSystem;
using UnityEngine;

[System.Serializable]
public class HeadThrowMagic : Spell
{
    [SerializeField] private string itemNeeded;
    [SerializeField] private string tarotNeeded;
    [SerializeField] private float attackMultiplier = 2f;

    public override void Use(Unit caster)
    {
        UseCost(caster);

        BattleSystem.Instance.UI.TextBox.SetText(magicText.GetLocalizedString(), () =>
        {
            // Check if is boss
            if (BattleSystem.Instance.EnemyUnit.Tags.HasTag("boss"))
            {
                foreach(var target in GetTargets())
                    caster.MagicAttack(target, attackMultiplier * Effectiveness);

                var playerTurn = BattleSystem.Instance.FSM.currentState as PlayerTurnState;
                playerTurn.EndTurn(1.5f);
            }
            else
            {
                foreach (var target in GetTargets())
                    target.Kill();
            }
        });
    }

    public override bool CostCheck(Unit caster)
    {
        TarotsManager tm = TarotsManager.Instance;

        if (tm && tm.HasTarot(tarotNeeded))
        {
            HangedManEffect hangedMan = tm.GetTarotByID(tarotNeeded).Effect as HangedManEffect;
            return hangedMan.HasStack();
        }
        else
        {
            if (GameManager.Instance.Player.Inventory.HasItem(itemNeeded))
                return true;
            else
                return ManaCheck(caster);
        }
    }

    public override void UseCost(Unit caster)
    {
        TarotsManager tm = TarotsManager.Instance;

        if (tm && tm.HasTarot(tarotNeeded))
        {
            if (tm.GetTarotByID(tarotNeeded).Effect is HangedManEffect hangedMan && hangedMan.HasStack())
            {
                hangedMan.UseStack();
            }
        }
        else
        {
            if (GameManager.Instance.Player.Inventory.HasItem(itemNeeded))
            {
                GameManager.Instance.Player.Inventory.RemoveItem(itemNeeded, 1);
            }
            else
            {
                ManaUse(caster);
            }
        }
    }
}
