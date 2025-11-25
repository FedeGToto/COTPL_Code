using UnityEngine;

public class PotionItem : ConsumableItem
{
    [SerializeField] private float value;
    [SerializeField] private bool isPercentage;

    public override void Use()
    {
        var player = GameManager.Instance.Player.Character;

        float healingValue = isPercentage ? (player.HealthPoints.Value * value) : value;
        player.HealthPoints.Value += healingValue;

        base.Use();
    }
}
