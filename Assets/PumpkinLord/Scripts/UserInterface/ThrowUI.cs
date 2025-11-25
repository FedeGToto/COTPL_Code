using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class ThrowUI : MonoBehaviour
{
    [SerializeField] private GameObject throwText;
    [SerializeField] private TextMeshProUGUI boxesToMove;

    private bool canBeThrown = true;

    private void Start()
    {
        EventManager.Instance.AddListener<DiceThrownEvent>(OnDiceThrown);
        EventManager.Instance.AddListener<MoveEvent>(OnMoveEvent);
        EventManager.Instance.AddListener<BoxLandEvent>(OnBoxLand);
    }

    public void ThrowDice()
    {
        if (!canBeThrown) return;
        ((GameManager.Instance.GameMode as BoardGameMode)).ThrowDices();
    }

    private void OnDiceThrown(DiceThrownEvent eventInfo)
    {
        canBeThrown = false;

        throwText.SetActive(false);
        boxesToMove.gameObject.SetActive(true);

        boxesToMove.text = "";
    }

    private void OnMoveEvent(MoveEvent eventInfo)
    {
        boxesToMove.text = eventInfo.RemainingBoxes.ToString();
    }

    private void OnBoxLand(BoxLandEvent eventInfo)
    {
        canBeThrown = true;

        throwText.SetActive(true);
        boxesToMove.gameObject.SetActive(false);
    }
}
