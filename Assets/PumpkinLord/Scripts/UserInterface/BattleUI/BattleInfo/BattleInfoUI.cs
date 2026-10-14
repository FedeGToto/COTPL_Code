using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class BattleInfoUI : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private CinemachineCamera cameraObject;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject battleHud;
    [SerializeField] private Button attackButton;

    [Header("Tabs")]
    [SerializeField] private TabMenu tabs;
    [SerializeField] private UnitInfoUI[] infoUIs;

    private bool isOpened;

    private void Start()
    {
        GameManager.Instance.Input.OnPause += OpenInfo;
    }
    private void OnDestroy()
    {
        GameManager.Instance.Input.OnPause -= OpenInfo;
    }

    public void OpenInfo()
    {
        isOpened = !isOpened;

        cameraObject.gameObject.SetActive(isOpened);
        infoPanel.SetActive(isOpened);
        battleHud.SetActive(!isOpened);

        if (isOpened)
        {
            foreach (var info in infoUIs)
                info.PopulatePanel();

            tabs.JumpToPage(1);
        }
        else
        {
            attackButton.Select();
        }
    }

    public void OnPageChange(int page)
    {
        infoUIs[page].SetPanelOn();
    }
}
