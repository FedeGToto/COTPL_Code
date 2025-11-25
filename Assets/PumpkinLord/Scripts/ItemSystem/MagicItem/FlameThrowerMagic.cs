using UnityEngine;

[System.Serializable]
public class FlameThrowerMagic : FireBallMagic
{
    [SerializeReference] private StatusEffect burnEffect;

    public override void Use()
    {
        base.Use();
        BattleSystem.Instance.EnemyUnit.Status.AddStatusEffect(burnEffect);
    }
}
