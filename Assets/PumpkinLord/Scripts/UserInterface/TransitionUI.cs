using DG.Tweening;
using System;
using UnityEngine;

public class TransitionUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Animation")]
    [SerializeField] private float animationDuration = 1f;

    public void FadeOut(Action onCompleted = default)
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, animationDuration).OnComplete(() =>
        {
            onCompleted?.Invoke();
        });
    }

    public void FadeIn(Action onCompleted = default)
    {
        canvasGroup.DOFade(0, animationDuration).OnComplete(() =>
        {
            onCompleted?.Invoke();
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });
    }
}
