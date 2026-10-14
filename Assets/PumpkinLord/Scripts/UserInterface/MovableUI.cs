using DG.Tweening;
using UnityEngine;

public class MovableUI : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private Vector2 outPosition;

    private RectTransform rect;
    private Vector2 inPosition;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        inPosition = rect.anchoredPosition;
    }

    public void Show()
    {
        rect.anchoredPosition = outPosition;
        gameObject.SetActive(true);
        rect.DOAnchorPos(inPosition, duration);
    }

    public void Hide()
    {
        rect.anchoredPosition = inPosition;
        rect.DOAnchorPos(outPosition, duration).OnComplete(() => gameObject.SetActive(false));
    }
}
