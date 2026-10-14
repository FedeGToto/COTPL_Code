using UnityEngine;
using UnityEngine.Localization;

public class Zone : MonoBehaviour
{
    [field: SerializeField] public string ZoneID;
    [SerializeReference] public ZoneEffect Effect;
}
