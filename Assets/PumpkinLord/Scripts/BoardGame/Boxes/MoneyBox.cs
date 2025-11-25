using UnityEngine;

public class MoneyBox : BoxEffect
{
    [SerializeField] private int moneyToAdd;

    public override void ApplyBoxEffect()
    {
        GameManager.Instance.Player.Money += moneyToAdd;
    }
}
