using DG.Tweening;
using UnityEngine;

public class HeadsUpDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform gameUI;

    [Header("Animation")]
    [SerializeField] private float gameUIHidePosition = -200f;
    [SerializeField] private float gameUIDuration = 0.5f;

    [Header("Shop UI")]
    [SerializeField] private ShopUI shop;

    private void Start()
    {
        EventManager.Instance.AddListener<StartDialogueEvent>(OnDialogueStart);
        EventManager.Instance.AddListener<FinishDialogueEvent>(OnDialogueEnded);

        EventManager.Instance.AddListener<OpenShopEvent>(OnOpenShop);
        EventManager.Instance.AddListener<CloseShopEvent>(OnCloseShop);
    }

    public void HideUI()
    {
        gameUI.DOAnchorPosY(gameUIHidePosition, gameUIDuration);
    }

    public void ShowUI()
    {
        gameUI.DOAnchorPosY(0, gameUIDuration);
    }

    private void OnDialogueStart(StartDialogueEvent e)
    {
        HideUI();
    }

    private void OnDialogueEnded(FinishDialogueEvent e)
    {
        ShowUI();
    }

    private void OnCloseShop(CloseShopEvent e)
    {
        ShowUI();
    }

    private void OnOpenShop(OpenShopEvent e)
    {
        HideUI();
        shop.OpenShop();
    }
}
