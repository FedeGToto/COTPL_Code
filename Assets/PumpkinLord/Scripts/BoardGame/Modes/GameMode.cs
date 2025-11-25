using Sirenix.OdinInspector;
using System;
using UnityEngine;

public abstract class GameMode
{
    public abstract void StartGame();

    // Battle Logic
    public abstract void StartBattle(EnemySO enemy);
    public abstract void StartBattle(string enemyId);
}
