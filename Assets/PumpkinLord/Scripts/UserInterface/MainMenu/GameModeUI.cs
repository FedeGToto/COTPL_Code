using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameModeUI : MonoBehaviour, IMenuPage
{
    [SerializeField] private LocalizedString[] modeDescriptions;
    [SerializeField] private Button[] gameModeButtons;

    [SerializeField] private TextMeshProUGUI descriptionText;

    public void ClosePage()
    {
        gameObject.SetActive(false);
    }

    public void OpenPage()
    {
        gameModeButtons[0].Select();
    }

    public void SelectButton(BaseEventData data)
    {
        int pos = Array.IndexOf(gameModeButtons, data.selectedObject.GetComponent<Button>());
        descriptionText.text = modeDescriptions[pos].GetLocalizedString();
    }

    public void OpenGameMode(string gameModeScene)
    {
        SceneManager.LoadScene(gameModeScene);
    }
}
