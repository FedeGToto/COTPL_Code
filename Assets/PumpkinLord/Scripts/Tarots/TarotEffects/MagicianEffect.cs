using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

public class MagicianEffect : TarotEffect
{
    [Header("Buff")]
    [SerializeReference] private StatusEffect spellBuff;
    [SerializeField] private LevelUpValues<float> effectivness;

    [Header("Debuff")]
    [SerializeReference] private StatusEffect spellDebuff;
    [SerializeField] private LevelUpValues<float> manaConsumption;

    public override string GetDescription(LocalizedString description, int level)
    {
        (description["meritValue"] as FloatVariable).Value = effectivness.Values[level] * 100f;
        (description["demeritValue"] as FloatVariable).Value = manaConsumption.Values[level] * 100f;

        return description.GetLocalizedString();
    }

    public override void BattleStart()
    {
        base.BattleStart();

        (spellBuff as SpellModifierEffect).ModifierValue = effectivness.Values[TarotsManager.Instance.GetLevel("tarot_magician") - 1];
        (spellDebuff as CostModifierEffect).ModifierValue = manaConsumption.Values[TarotsManager.Instance.GetLevel("tarot_magician") - 1];
        BattleSystem.Instance.PlayerUnit.Status.AddStatusEffect(spellBuff);
        BattleSystem.Instance.PlayerUnit.Status.AddStatusEffect(spellDebuff);
    }
}
