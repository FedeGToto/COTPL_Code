using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

public class HighPriestessEffect : TarotEffect
{
    [Header("Dodge")]
    [SerializeField] private LevelUpValues<float> dodgeValues;
    [SerializeReference] private StatusEffect dodgeEffect;

    [Header("Insanity")]
    [SerializeField] private LevelUpValues<int> insanityStacks;
    [SerializeReference] private StatusEffect insanityStatus;

    public override string GetDescription(LocalizedString description, int level)
    {
        if (description.TryGetValue("meritValue", out var meritValue))
        {
            (meritValue as FloatVariable).Value = dodgeValues.Values[level] * 100f;
        }

        if (description.TryGetValue("demeritValue", out var demeritValue))
        {
            (demeritValue as IntVariable).Value = insanityStacks.Values[level];
        }

        return description.GetLocalizedString();
    }

    public override void BattleStart()
    {
        base.BattleStart();

        (dodgeEffect as StatBoostEffect).Value = dodgeValues.Values[TarotsManager.Instance.GetLevel("tarot_highpriestess") - 1];
        BattleSystem.Instance.PlayerUnit.Status.AddStatusEffect(dodgeEffect);
    }

    public override void OnTurnStart()
    {
        base.OnTurnStart();

        (insanityStatus as InsanityEffect).StacksToAdd = insanityStacks.Values[TarotsManager.Instance.GetLevel("tarot_highpriestess") - 1];
        BattleSystem.Instance.PlayerUnit.Status.AddStatusEffect(insanityStatus);
    }

    public override void BattleEnded()
    {
        base.BattleEnded();
        BattleSystem.Instance.PlayerUnit.Status.RemoveStatusEffect(insanityStatus);
        BattleSystem.Instance.PlayerUnit.Status.RemoveStatusEffect(dodgeEffect);
    }
}
