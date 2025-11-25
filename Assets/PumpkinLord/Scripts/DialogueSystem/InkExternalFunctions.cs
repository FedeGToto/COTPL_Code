using Ink.Runtime;
using System;
using UnityEngine;

public class InkExternalFunctions
{
    public void Bind(Story story)
    {
        story.BindExternalFunction("MoveTo", (string boxId) => MoveTo(boxId));
        story.BindExternalFunction("StartBattle", (string enemyId) => StartBattle(enemyId));
    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("MoveTo");
        story.UnbindExternalFunction("StartBattle");
    }

    public void MoveTo(string boxId)
    {
        Type box;

        box = boxId switch
        {
            "MoneyBox" => typeof(MoneyBox),
            "EnemyBox" => typeof(EnemyBox),
            "ChurchBox" => typeof(ChurchBox),
            "BonfireBox" => typeof(BonfireBox),
            "ShopBox" => typeof(ShopBox),
            _ => null
        };

        if (box != null)
            (GameManager.Instance.GameMode as BoardGameMode).GetNextBoxOfType(box);
    }

    public void StartBattle(string enemyId)
    {
        GameManager.Instance.GameMode.StartBattle(enemyId);
    }
}
