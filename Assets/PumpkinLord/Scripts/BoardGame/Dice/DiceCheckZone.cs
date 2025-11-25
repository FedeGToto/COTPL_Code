using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZone : MonoBehaviour
{
    public Dice[] dices;

    public GameManager gameManager;

    private void OnTriggerStay(Collider other)
    {
        for (int i = 0; i < dices.Length; i++)
        {
            if (other.transform.parent.gameObject == dices[i].gameObject)
            {
                if (!dices[i].IsRolling)
                {

                    if (int.TryParse(other.gameObject.name, out int number))
                    {
                        (gameManager.GameMode as BoardGameMode).SetDiceThrow(i, number);
                    }
                }
            }
        }
    }
}
