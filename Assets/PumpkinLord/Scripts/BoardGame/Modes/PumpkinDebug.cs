using QFSW.QC;
using UnityEngine;

public class PumpkinDebug : MonoBehaviour
{
    PumpkinMode currentMode;

    private void Start()
    {
        currentMode = GetComponent<GameManager>().GameMode as PumpkinMode;
    }

    [Command]
    public void MovePlayer(int steps)
    {
        currentMode.MovePlayer(steps);
    }

    [Command]
    public void Paycheck(int money)
    {
        GetComponent<GameManager>().Player.Money += money;
    }

    [Command]
    public void SetMoney(int money)
    {
        GetComponent<GameManager>().Player.Money = money;
    }
}
