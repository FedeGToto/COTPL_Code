using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseSystem : MonoBehaviour
{
    public bool CanPause = true;

    [SerializeField] private InputManager input;

    [Header("UI")]
    [SerializeField] private PauseUI pause;
    
    private bool isPaused = false;

    private void Start()
    {
        input.OnPause += PausePressed;
    }
    private void OnDestroy()
    {
        input.OnPause -= PausePressed;
    }

    public void PausePressed()
    {
        if (!CanPause) return;

        isPaused = !isPaused;

        InputManager.InputContext context = isPaused ? InputManager.InputContext.UserInterface : InputManager.InputContext.Default;
        input.ChangeContext(context);

        pause.SetPauseMenu(isPaused);
    }
}
