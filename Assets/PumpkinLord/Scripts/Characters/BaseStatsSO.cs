using UnityEngine;

[CreateAssetMenu(fileName = "NewBaseStats", menuName = "Pumpkin Lord/Characters/Base Stats")]
public class BaseStatsSO : ScriptableObject
{
    [field:SerializeField] public int HealthPoints { get; private set; }
    [field:SerializeField] public int ManaPoints { get; private set; }
    [field:SerializeField] public int PhysicalAttack { get; private set; }
    [field:SerializeField] public int PhysicalArmor { get; private set; }
    [field:SerializeField] public int MagicAttack { get; private set; }
    [field:SerializeField] public int MagicArmor { get; private set; }
}
