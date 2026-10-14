using DG.Tweening;
using UnityEngine;

public class PlayerInfoUI : MonoBehaviour
{
    [field: SerializeField] public MovableUI DiceThrow { get; private set; }
    [field: SerializeField] public MovableUI Stats { get; private set; }
    [field: SerializeField] public MovableUI Currencies { get; private set; }
    [field: SerializeField] public MovableUI Health { get; private set; }

    public void ShowAll()
    {
        DiceThrow.Show();
        Stats.Show();
        Currencies.Show();
        Health.Show();
    }

    public void HideAll()
    {
        DiceThrow.Hide();
        Stats.Hide();
        Currencies.Hide();
        Health.Hide();
    }
}
