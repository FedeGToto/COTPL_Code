using DG.Tweening;
using UnityEngine;

public class BottomUI : MonoBehaviour
{
    [SerializeField] private PlayerActionsUI actions;
    [field: SerializeField] public UseListUI UseList { get; private set; }

    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private float outScreenPos = -264;

    public void SetOn()
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, outScreenPos);
        rect.DOAnchorPosY(0, animationDuration).OnComplete(() =>
        {
            actions.EnableButtons();
        });
    }

    public void SetOff()
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, 0);

        UseList.gameObject.SetActive(false);

        rect.DOAnchorPosY(outScreenPos, animationDuration).OnComplete(() =>
        {
            actions.DisableButton();
        });
    }
}
