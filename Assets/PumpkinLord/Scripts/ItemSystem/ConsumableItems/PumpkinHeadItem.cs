using StatSystem;

public class PumpkinHeadItem : ConsumableItem, IDeathSaver
{
    public int Priority => 10;

    public override void BattleStart()
    {
        base.BattleStart();
        BattleSystem.Instance.PlayerUnit.RegisterDeathSave(this);
    }

    public override void BattleEnded()
    {
        base.BattleEnded();
        BattleSystem.Instance.PlayerUnit.UnregisterDeathSave(this);
    }

    public bool TrySave(Resource health)
    {
        float healValue = health.MaxValue;
        health.Value = healValue;

        // effect

        Use();
        return true;
    }
}
