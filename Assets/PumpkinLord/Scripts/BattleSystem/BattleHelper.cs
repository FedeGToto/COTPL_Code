using UnityEngine;

public static class BattleHelper 
{
    public static float CalculateDamage(float attack, float defense)
    {
        float dmg = attack - (defense / 2);
        return Mathf.Clamp(dmg, 1, float.MaxValue);
    }
}
