using UnityEngine;

[CreateAssetMenu(fileName = "Box", menuName = "Pumpkin Lord/Board Game/Box")]

public class BoxSO : ScriptableObject
{
    [field: SerializeField] public BoxEffect Effect { get; private set; }
}
