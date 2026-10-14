using UnityEngine;

public class CostModifierEffect : StatusEffect
{
    public float ModifierValue;

    public override void OnAdd(Unit unit)
    {
        base.OnAdd(unit);

        //(StatusDescription["value"] as FloatVariable).Value = ModifierValue;
    }


    public override void OnTurnStart()
    {
        if (BattleSystem.Instance.FSM.currentState is PlayerTurnState)
        {
            var magicItems = GameManager.Instance.Player.Inventory.GetMagicItems();

            foreach (var item in magicItems)
            {
                (item.Effect as MagicItem).Spell.CostMultiplier += ModifierValue;
            }
        }
        else if (BattleSystem.Instance.FSM.currentState is EnemyTurnState)
        {
            // Get magics and change the cost multiplier
        }
    }

    public override void OnTurnEnd()
    {
        if (BattleSystem.Instance.FSM.currentState is PlayerTurnState)
        {
            var magicItems = GameManager.Instance.Player.Inventory.GetMagicItems();

            foreach (var item in magicItems)
            {
                (item.Effect as MagicItem).Spell.CostMultiplier -= ModifierValue;
            }
        }
        else if (BattleSystem.Instance.FSM.currentState is EnemyTurnState)
        {
            // Get magics and change the cost multiplier
        }
    }
}
