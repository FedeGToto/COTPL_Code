using System;
using UnityEngine;

public abstract class BoardGameMode : GameMode
{
    [SerializeField] protected BoardBox[] boxes;
    [SerializeField] protected Pawn playerPawn;

    public override void StartGame()
    {
        InitializeBoard();
    }

    public abstract void InitializeBoard();
    public abstract void StartNextTurn();
    public abstract BoardBox GetBox();
    public abstract void GetNextBoxOfType(Type box);
    public abstract void MovePlayerToNextPosition();

    // Dices logic
    public abstract void ThrowDices();
    public abstract void SetDiceThrow(int diceId, int diceThrow);
}
