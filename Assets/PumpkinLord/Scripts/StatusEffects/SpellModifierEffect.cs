using System;
using UnityEngine;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

public class SpellModifierEffect : StatusEffect
{
    public float ModifierValue;

    public override void OnAdd(Unit unit)
    {
        base.OnAdd(unit);

        BattleSystem.Instance.PlayerUnit.OnSpellUsed.AddListener(SpellBuff);
        //(StatusDescription["value"] as FloatVariable).Value = ModifierValue;
    }

    public override void OnRemove(Unit unit)
    {
        BattleSystem.Instance.PlayerUnit.OnSpellUsed.RemoveListener(SpellBuff);
    }

    private void SpellBuff(Spell spell)
    {
        spell.Effectiveness = ModifierValue;
    }
}
