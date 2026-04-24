using Unity.Cinemachine;
using UnityEngine;

public class ShopBox : BoxEffect
{
    [SerializeField] private CinemachineCamera shopCamera;

    public override void ApplyBoxEffect()
    {
        shopCamera.gameObject.SetActive(true);
        EventManager.Instance.AddListener<CloseShopEvent>(OnShopClose);
        EventManager.Instance.TriggerEvent(new OpenShopEvent());
    }

    private void OnShopClose(CloseShopEvent e)
    {
        shopCamera.gameObject.SetActive(false);
        (GameManager.Instance.GameMode as BoardGameMode).StartNextTurn();
    }
}
