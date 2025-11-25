using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "New Tag", menuName = "Pumpkin Lord/Tag")]
public class TagSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public LocalizedString DisplayName { get; private set; }
}
