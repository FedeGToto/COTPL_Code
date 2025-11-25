using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI soulsText;

    [SerializeField] private ResourceBar healthBar;
    [SerializeField] private ResourceBar manaBar;

    [Header("Animations")]
    [SerializeField] private float animationDuration = 0.3f;

    private void Start()
    {
        moneyText.text = "0";
        soulsText.text = "0";

        healthBar.SetupBar(GameManager.Instance.Player.Character.HealthPoints);
        manaBar.SetupBar(GameManager.Instance.Player.Character.ManaPoints);

        EventManager.Instance.AddListener<MoneyChangeEvent>(OnMoneyChanged);
        EventManager.Instance.AddListener<SoulsChangeEvent>(OnSoulsChanged);
    }

    private void OnMoneyChanged(MoneyChangeEvent e)
    { 
        moneyText.DOCounter(int.Parse(soulsText.text), e.Money, animationDuration, false);
    }

    private void OnSoulsChanged(SoulsChangeEvent e)
    {
        soulsText.DOCounter(int.Parse(soulsText.text), e.Souls, animationDuration, false);
    }

}
