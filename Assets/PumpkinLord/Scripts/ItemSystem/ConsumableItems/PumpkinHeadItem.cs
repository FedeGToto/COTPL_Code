using System;
using UnityEngine;

public class PumpkinHeadItem : ConsumableItem
{
    public override void BattleStart()
    {
        base.BattleStart();
        BattleSystem.Instance.PlayerUnit.OnTakeDamage.AddListener(OnDamageTaken);
    }

    public override void BattleEnded()
    {
        base.BattleEnded();
        BattleSystem.Instance.PlayerUnit.OnTakeDamage.RemoveListener(OnDamageTaken);
    }

    private void OnDamageTaken()
    {
        if (BattleSystem.Instance.PlayerUnit.Character.HealthPoints.Value <= 0)
        {
            Use();
        }
    }

    public override void Use()
    {
        var player = GameManager.Instance.Player;

        float healValue = player.Character.HealthPoints.MaxValue;
        player.Character.HealthPoints.Value = healValue;

        // Use effect

        base.Use();
    }
}
