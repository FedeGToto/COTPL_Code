using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class BattleUI : MonoBehaviour
{
    [field: SerializeField] public List<LocalizedString> StarterTextes;
    [field: SerializeField] public LocalizedString WinText { get; private set; }
    [field: SerializeField] public LocalizedString EnemyTurnText { get; private set; }

    [Header("HUD")]
    [field: SerializeField] public ActionTextUI TextBox;
    [field: SerializeField] public BottomUI Bottom { get; private set; }
    [field: SerializeField] public UnitUI PlayerUI { get; private set; }
    [SerializeField] private UnitUI enemyUI;
    [SerializeField] private UnitUI bossUI;
    
    public UnitUI EnemyUI => isBoss ? bossUI : enemyUI;

    private bool isBoss;

    public void StartBattle(bool isBoss = false)
    {
        this.isBoss = isBoss;

        PlayerUI.Setup();

        if (isBoss)
        {
            bossUI.Setup();
            bossUI.SetName();
        }
        else
            enemyUI.Setup();
    }

    public string PickStarterText(string enemyName)
    {
        int id = Random.Range(0, StarterTextes.Count);

        var dict = new Dictionary<string, string>() { { "name", enemyName } };
        return StarterTextes[id].GetLocalizedString(dict);
    }
}
