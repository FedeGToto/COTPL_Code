using StatSystem;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Stats")]
    public BaseStatsSO BaseStats;

    public Dictionary<string, Stat> Stats = new();

    public Resource HealthPoints { get; private set; }
    public Resource ManaPoints { get; private set; }


    private void Awake()
    {
        if (BaseStats != null)
            SetupStats();
    }

    public void SetupStats(BaseStatsSO stats)
    {
        BaseStats = stats;
        SetupStats();
    }

    public void SetupStats()
    {
        if (BaseStats == null)
        {
            Debug.LogError("This character does not have Base Stats");
            return;
        }

        HealthPoints = new Resource(BaseStats.HealthPoints);
        ManaPoints = new Resource(BaseStats.ManaPoints);

        Stats.Add("attack_phy", new(BaseStats.PhysicalAttack));
        Stats.Add("attack_mag", new(BaseStats.PhysicalArmor));
        Stats.Add("defense_phy", new(BaseStats.MagicAttack));
        Stats.Add("defense_mag", new(BaseStats.MagicArmor));
    }
}
