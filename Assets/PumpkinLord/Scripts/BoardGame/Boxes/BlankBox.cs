using UnityEngine;

public class BlankBox : BoxEffect
{
    public override void ApplyBoxEffect()
    {
        (GameManager.Instance.GameMode as BoardGameMode).StartNextTurn();
    }
}
