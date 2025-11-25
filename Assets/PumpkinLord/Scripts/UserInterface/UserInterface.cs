using System;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    [field: SerializeField] public HeadsUpDisplay HUD { get; private set; }
    [field: SerializeField] public PauseUI Pause { get; private set; }

    private void Start()
    {
        EventManager.Instance.AddListener<StartBattleEvent>(OnBattleStart);
        EventManager.Instance.AddListener<BattleEndedEvent>(OnBattleEnded);
    }

    private void OnBattleStart(StartBattleEvent e)
    {
        Hide();
    }

    private void OnBattleEnded(BattleEndedEvent e)
    {
        Show();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
