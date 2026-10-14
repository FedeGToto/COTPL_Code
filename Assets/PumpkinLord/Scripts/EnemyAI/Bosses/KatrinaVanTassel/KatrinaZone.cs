using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class KatrinaZone : ZoneEffect
{
    [SerializeField] private float stackConversion = 0.1f;
    [SerializeReference] private StatusEffect insanityEffect;

    [Header("Textes")]
    [SerializeField] private LocalizedString openText;
    [SerializeField] private LocalizedString closeText;
    [SerializeField] private LocalizedString updateText;

    private Dictionary<Unit, float> storedDamage;
    private int currentTurns;

    public float TotalDamage
    {
        get
        {
            float dmg = 0;

            foreach (var damage in storedDamage)
            {
                dmg += damage.Value;
            }

            return dmg;
        }
    }

    public override void OpenZone()
    {
        base.OpenZone();

        storedDamage = new Dictionary<Unit, float>
        {
            { BattleSystem.Instance.PlayerUnit, 0 },
            { BattleSystem.Instance.EnemyUnit, 0 }
        };

        BattleSystem.Instance.PlayerUnit.OnDamageDealt.AddListener(OnDamageDealt);
        BattleSystem.Instance.EnemyUnit.OnDamageDealt.AddListener(OnDamageDealt);

        BattleSystem.Instance.UI.TextBox.SetText(currentTurns.ToString());
    }

    public override void OnTurnStart(Unit unit)
    {
        base.OnTurnStart(unit);

        if (unit == BattleSystem.Instance.EnemyUnit)
            currentTurns++;

        BattleSystem.Instance.UI.TextBox.SetText(currentTurns.ToString());
    }

    public override void OnTurnEnd(Unit unit)
    {
        base.OnTurnEnd(unit);

        if (currentTurns >= 5)
        {
            int stacks = Mathf.RoundToInt(TotalDamage * stackConversion);
            (insanityEffect as InsanityEffect).Stacks = stacks;

            Unit target = GetTarget();
            target.Status.AddStatusEffect(insanityEffect);

            foreach (var damage in storedDamage)
            {
                if (damage.Key == target)
                    continue;

                damage.Key.Status.RemoveStatusEffect(typeof(InsanityEffect));
            }
            BattleSystem.Instance.CloseZone();
        }
    }

    private void OnDamageDealt(DamageInfo info)
    {
        storedDamage[info.Source] += info.FinalDamage;
    }

    private Unit GetTarget()
    {
        Unit selectedUnit = null;
        float damage = float.MaxValue;

        foreach (var item in storedDamage)
        {
            if (item.Value < damage)
            {
                selectedUnit = item.Key;
                damage = item.Value;
            }
        }

        return selectedUnit;
    }
}
