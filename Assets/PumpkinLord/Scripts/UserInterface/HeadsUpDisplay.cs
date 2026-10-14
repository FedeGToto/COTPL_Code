using DG.Tweening;
using UnityEngine;

public class HeadsUpDisplay : MonoBehaviour
{
    [SerializeField] private PlayerInfoUI playerInfoUI;

    [Header("Shop UI")]
    [SerializeField] private ShopUI shop;

    private void Start()
    {
        EventManager.Instance.AddListener<StartDialogueEvent>(OnDialogueStart);
        EventManager.Instance.AddListener<FinishDialogueEvent>(OnDialogueEnded);

        EventManager.Instance.AddListener<OpenShopEvent>(OnOpenShop);
        EventManager.Instance.AddListener<CloseShopEvent>(OnCloseShop);
    }

    private void OnDialogueStart(StartDialogueEvent e)
    {
        playerInfoUI.HideAll();
    }

    private void OnDialogueEnded(FinishDialogueEvent e)
    {
        playerInfoUI.ShowAll();
    }

    private void OnCloseShop(CloseShopEvent e)
    {
        playerInfoUI.Health.Show();
        playerInfoUI.DiceThrow.Show();
        playerInfoUI.Stats.Show();
    }

    private void OnOpenShop(OpenShopEvent e)
    {
        playerInfoUI.Health.Hide();
        playerInfoUI.DiceThrow.Hide();
        playerInfoUI.Stats.Hide();

        shop.OpenShop();
    }
}
