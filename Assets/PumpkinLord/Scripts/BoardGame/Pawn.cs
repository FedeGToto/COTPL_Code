using DG.Tweening;
using System;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public void MoveTo(Transform landPoint)
    {
        transform.DOJump(landPoint.position, 1, 1, 0.5f).OnComplete(() =>
        {
            (GameManager.Instance.GameMode as BoardGameMode).MovePlayerToNextPosition();
        });
    }

}
