using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button firstSelectable;

    [SerializeField] private GameModeUI gameModeUI;
    [SerializeField] private EncyclopediaUI encyclopediaUI;
    [SerializeField] private ErrorUI errorUI;

    [Header("Quit Game")]
    [SerializeField] private LocalizedString quitTitle;
    [SerializeField] private LocalizedString quitDescription;
    [SerializeField] private LocalizedString confirmText;
    [SerializeField] private LocalizedString cancelText;

    private IMenuPage currentMenuPage;

    private void Start()
    {
        firstSelectable.Select();
    }

    public void OpenGameModes()
    {
        currentMenuPage?.ClosePage();
        currentMenuPage = gameModeUI;
        gameModeUI.gameObject.SetActive(true);
        currentMenuPage?.OpenPage();
    }

    public void OpenEncyclopedia()
    {
        currentMenuPage?.ClosePage();
        currentMenuPage = encyclopediaUI;
        encyclopediaUI.gameObject.SetActive(true);
        currentMenuPage?.OpenPage();
    }

    public void OpenSettings()
    {

    }

    public void QuitGame()
    {
        errorUI.ShowMessage(quitTitle.GetLocalizedString(), quitDescription.GetLocalizedString(), 
            (confirmText.GetLocalizedString(), () => {
                Application.Quit();
            }),
            (cancelText.GetLocalizedString(), () =>
            {
                firstSelectable.Select();
            })
        );
    }

}
