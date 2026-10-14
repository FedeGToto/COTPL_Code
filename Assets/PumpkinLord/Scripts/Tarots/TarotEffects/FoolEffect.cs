using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

public class FoolEffect : TarotEffect, IDeathSaver
{
    [SerializeField] private LevelUpValues<float> missChance;
    [SerializeReference] private StatusEffect demeritEffects;

    public int Priority { get; } = 999;
    private int uses;

    public override void OnTarotAdded()
    {
        base.OnTarotAdded();
        uses++;
    }

    public override void BattleStart()
    {
        base.BattleStart();
        BattleSystem.Instance.PlayerUnit.RegisterDeathSave(this);

        (demeritEffects as StatBoostEffect).Value = missChance.Values[0];
        BattleSystem.Instance.PlayerUnit.Status.AddStatusEffect(demeritEffects);
    }

    public override void BattleEnded()
    {
        base.BattleEnded();
        BattleSystem.Instance.PlayerUnit.UnregisterDeathSave(this);
        
        BattleSystem.Instance.PlayerUnit.Status.RemoveStatusEffect(demeritEffects);
    }

    public bool TrySave(StatSystem.Resource health)
    {
        if (uses <= 0) return false;
        uses--;

        float healValue = health.MaxValue * 0.5f;
        health.Value = healValue;

        return true;
    }

    public override string GetDescription(LocalizedString description, int level)
    {
        (description["value"] as FloatVariable).Value = (missChance.Values[level] * 100f);
        return description.GetLocalizedString();
    }
}
