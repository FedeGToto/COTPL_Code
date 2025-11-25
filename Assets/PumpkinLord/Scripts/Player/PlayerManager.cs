using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerManager : MonoBehaviour
{
    [field: SerializeField] public Character Character;
    [field: SerializeField] public Inventory Inventory;
    [field: SerializeField] public GameObject Pawn;

    private int money;
    private int souls;

    public int Money
    {
        get => money;
        set
        {
            money = value; 
            EventManager.Instance.TriggerEvent(new MoneyChangeEvent() { Money = money });
        }
    }
    public int Souls
    {
        get => souls;
        set
        {
            souls = value;
            EventManager.Instance.TriggerEvent(new SoulsChangeEvent() { Souls = souls });
        }
    }


}
