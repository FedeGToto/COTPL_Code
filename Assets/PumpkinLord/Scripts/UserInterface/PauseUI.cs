using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private PauseSystem pause;

    [Header("UI")]
    [SerializeField] private CanvasGroup menuCanvas;
    [SerializeField] private Button resume;

    [Header("Animation")]
    [SerializeField] private float openDuration = 0.2f;

    public void SetPauseMenu(bool isPaused)
    {
        gameObject.SetActive(true);

        float finalAlpha = isPaused ? 1.0f : 0.0f;

        menuCanvas.DOFade(finalAlpha, openDuration).OnComplete(() =>
        {
            if (isPaused)
            {
                resume.Select();
            }

            if (!isPaused)
                gameObject.SetActive(false);
        });
    }

    public void Resume()
    {
        pause.PausePressed();
    }
}
