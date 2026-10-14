using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float durationPerCharacter = 0.05f;

    private readonly Queue<(string message, Action onComplete)> textQueue = new();
    private Tween currentTween;
    private bool isPlaying;

    public void SetText(string message, Action onComplete = default)
    {
        textQueue.Enqueue((message, onComplete));

        if (!isPlaying)
            PlayNext();
    }

    private void PlayNext()
    {
        if (textQueue.Count == 0)
        {
            isPlaying = false;
            return;
        }

        isPlaying = true;
        var (message, onComplete) = textQueue.Dequeue();

        text.SetText("");
        float textDuration = message.Length * durationPerCharacter;

        currentTween = text.DOText(message, textDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                onComplete?.Invoke();
                PlayNext();
            });
    }

    public void Clear()
    {
        textQueue.Clear();
        currentTween?.Kill();
        isPlaying = false;
        text.SetText("");
    }

    private void OnDestroy()
    {
        currentTween?.Kill();
    }
}
