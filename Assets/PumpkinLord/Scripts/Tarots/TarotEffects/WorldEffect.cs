using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

public class WorldEffect : TarotEffect
{
    [Header("Merit")]
    [SerializeField] private int meritThreshold;
    [SerializeReference] private StatusEffect meritEffect;
    [SerializeField] private LevelUpValues<float> meritValues;

    [Header("Demerit")]
    [SerializeField] private int demeritThreshold;
    [SerializeReference] private StatusEffect demeritEffect;
    [SerializeField] private LevelUpValues<float> demeritValues;

    public override string GetDescription(LocalizedString description, int level)
    {
        (description["meritValue"] as FloatVariable).Value = meritValues.Values[level] * 100f;
        (description["demeritValue"] as FloatVariable).Value = demeritValues.Values[level] * -100f;

        return description.GetLocalizedString();
    }

    public override void BattleStart()
    {
        base.BattleStart();

        int level = TarotsManager.Instance.GetLevel("tarot_world");

        if (GameManager.Instance.Player.Souls < demeritThreshold)
        {
            (demeritEffect as AttackBoostEffect).BoostMultiplier = demeritValues.Values[level - 1];
            (demeritEffect.StatusDescription["value"] as FloatVariable).Value = demeritValues.Values[level - 1];
            BattleSystem.Instance.PlayerUnit.Status.AddStatusEffect(demeritEffect);
        }
        else if (GameManager.Instance.Player.Souls >= meritThreshold)
        {
            (meritEffect as AttackBoostEffect).BoostMultiplier = meritValues.Values[level - 1];
            (meritEffect.StatusDescription["value"] as FloatVariable).Value = meritValues.Values[level - 1];
            BattleSystem.Instance.PlayerUnit.Status.AddStatusEffect(meritEffect);
        }
    }
}
