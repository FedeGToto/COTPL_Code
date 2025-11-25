using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class BattleUI : MonoBehaviour
{
    [field: SerializeField] public List<LocalizedString> StarterTextes;

    [Header("HUD")]
    [field: SerializeField] public ActionTextUI TextBox;
    [field: SerializeField] public BottomUI Bottom { get; private set; }
    [field: SerializeField] public UnitUI PlayerUI { get; private set; }
    [field: SerializeField] public UnitUI EnemyUI { get; private set; }

    [Header("Textes")]
    [field: SerializeField] public LocalizedString WinText { get; private set; }
    [field: SerializeField] public LocalizedString EnemyTurnText { get; private set; }

    public void StartBattle()
    {
        PlayerUI.Setup();
        EnemyUI.Setup();

    }

    public string PickStarterText(string enemyName)
    {
        int id = Random.Range(0, StarterTextes.Count);

        var dict = new Dictionary<string, string>() { { "name", enemyName } };
        return StarterTextes[id].GetLocalizedString(dict);
    }
}
