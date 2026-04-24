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

    [Command("heal")]
    public void Heal(Unit character, float value)
    {
        character.Heal(value);
    }

    [Command("set-speed")]
    public void SetSpeed(float speed = 1)
    {
        Time.timeScale = speed;
    }
    #endregion
}
