using UnityEngine;

public class EnemyBox : BoxEffect
{
    [SerializeField] private EnemySO[] enemyPool;

    public override void ApplyBoxEffect()
    {
        int randomEnemy = Random.Range(0, enemyPool.Length);
        EnemySO enemy = enemyPool[randomEnemy];

        GameManager.Instance.GameMode.StartBattle(enemy);
    }

}
