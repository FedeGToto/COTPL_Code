using QFSW.QC;
using QFSW.QC.Suggestors.Tags;
using System;
using UnityEngine;

public class PumpkinDebug : MonoBehaviour
{
    [SerializeField] private ItemSO[] items;

    PumpkinMode currentMode;

    private void Start()
    {
        currentMode = GetComponent<GameManager>().GameMode as PumpkinMode;
    }

    [Command("move")]
    public void MovePlayer(int steps)
    {
        currentMode.MovePlayer(steps);
    }

    [Command("move-to")]
    public void MovePlayer(
        [Suggestions("MoneyBox", "EnemyBox", "ChurchBox", "BonfireBox", "ShopBox", "HorseBox", "BlankBox")]string boxId)
    {
        Type box;

        box = boxId switch
        {
            "MoneyBox" => typeof(MoneyBox),
            "EnemyBox" => typeof(EnemyBox),
            "ChurchBox" => typeof(ChurchBox),
            "BonfireBox" => typeof(BonfireBox),
            "ShopBox" => typeof(ShopBox),
            "HorseBox" => typeof(HorseBox),
            "BlankBox" => typeof(BlankBox),
            _ => null
        };

        if (box != null)
            (GameManager.Instance.GameMode as BoardGameMode).GetNextBoxOfType(box);
    }

    [Command("paycheck")]
    public void Paycheck(int money)
    {
        GetComponent<GameManager>().Player.Money += money;
    }

    [Command("set-money")]
    public void SetMoney(int money)
    {
        GetComponent<GameManager>().Player.Money = money;
    }

    [Command("set-souls")]
    public void SetSouls(int souls)
    {
        GetComponent<GameManager>().Player.Souls = souls;
    }

    [Command("give")]
    public void GiveItem(string itemId)
    {
        foreach (var itemSO in items)
        {
            if (itemId == itemSO.ID)
            {
                GetComponent<GameManager>().Player.Inventory.AddItem(itemSO);
                return;
            }
        }

        Debug.LogWarning($"An item with id {itemId} does not exists.");
    }
}
