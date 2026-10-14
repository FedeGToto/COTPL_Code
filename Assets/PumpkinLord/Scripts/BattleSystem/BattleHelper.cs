using UnityEngine;

public static class BattleHelper 
{
    public static float CalculateDamage(float attack, float defense)
    {
        float dmg = attack * (attack / (attack + defense));
        return Mathf.Max(1, dmg);
    }
}
