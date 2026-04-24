using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Unit Unit { get; private set; }
    [SerializeReference] public EnemyAI AI;

    [SerializeField] private TagSO[] tags;

    [Header("Rewards")]
    [field: SerializeField] public int Money { get; private set; }
    [field: SerializeField] public int Souls { get; private set; } = 1;

    public void Setup(Unit unit)
    {
        Unit = unit;

        foreach (TagSO tag in tags)
        {
            Unit.Tags.AddTag(tag);
        }

        unit.OnDie.AddListener(Die);

        AI.Setup(this);

        Unit.AttacksDescription = AI.GetAttacksDescription();
    }

    public void Die()
    {
        BattleSystem.Instance.FSM.ChangeState(new WinBattleState());
    }
}
