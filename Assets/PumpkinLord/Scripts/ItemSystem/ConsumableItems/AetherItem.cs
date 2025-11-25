using UnityEngine;

public class AetherItem : ConsumableItem
{
    [SerializeField] private float value;
    [SerializeField] private bool isPercentage;

    public override void Use()
    {
        var player = GameManager.Instance.Player.Character;

        float healingValue = isPercentage ? (player.ManaPoints.Value * value) : value;
        player.ManaPoints.Value += healingValue;

        base.Use();
    }
}
