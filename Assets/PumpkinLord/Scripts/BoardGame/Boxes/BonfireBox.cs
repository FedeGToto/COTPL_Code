using MoreMountains.Feedbacks;
using UnityEngine;

public class BonfireBox : BoxEffect
{
    [SerializeField, Range(0, 1)] private float healingPercentage = 0.1f;
    [SerializeField] private MMF_Player healingAnimation;

    public override void ApplyBoxEffect()
    {
        Character player = GameManager.Instance.Player.Character;
        float maxHealth = player.HealthPoints.MaxValue;
        float maxMana = player.ManaPoints.MaxValue;

        float health = maxHealth * healingPercentage;
        float mana = maxMana * healingPercentage;

        player.HealthPoints.Value += health;
        player.ManaPoints.Value += health;

        healingAnimation.PlayFeedbacks();

        (GameManager.Instance.GameMode as BoardGameMode).StartNextTurn();
    }
}
