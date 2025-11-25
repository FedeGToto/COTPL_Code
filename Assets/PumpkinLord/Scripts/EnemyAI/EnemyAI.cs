using UnityEngine;

[SerializeField]
public partial class EnemyAI
{
    public Enemy Owner { get; private set; }

    public virtual void Setup(Enemy owner)
    {
        Owner = owner;
    }

    public virtual void EvaluateNextAction() { }
}
