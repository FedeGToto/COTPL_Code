using DG.Tweening;
using TMPro;
using UnityEngine;

public class ActionTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float durationPerCharacter = 0.05f;

    public Tween SetText(string text)
    {
        this.text.text = "";
        float textDuration = text.Length * durationPerCharacter;
        return this.text.DOText(text, textDuration);
    }
}
