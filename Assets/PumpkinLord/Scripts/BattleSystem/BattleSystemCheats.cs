using QFSW.QC;
using UnityEngine;

public class BattleSystemCheats : MonoBehaviour
{
     private BattleSystem parent;

    private void Start()
    {
        parent = GetComponent<BattleSystem>();
    }

    #region Cheats
    [Command("auto-win")]
    public void InstaWin()
    {
        parent.EnemyUnit.Kill();
    }

    [Command("damage")]
    public void Damage(Unit character, float value)
    {
        DamageInfo info = new()
        {
            Target = character,
            Source = character,
            DamageType = DamageType.Physical,
            BaseDamage = value
        };
        character.TakeDamage(info);
    }

    [Command("heal")]
    public void Heal(Unit character, float value)
    {
        character.Heal(value);
    }

    [Command("restore-mana")]
    public void RestoreMana(Unit character, float value)
    {
        character.Character.ManaPoints.Value += value;
    }

    [Command("deplete-mana")]
    public void DepleteMana(Unit character, float value)
    {
        character.Character.ManaPoints.Value -= value;
    }

    [Command("set-speed")]
    public void SetSpeed(float speed = 1)
    {
        Time.timeScale = speed;
    }
    #endregion
}
