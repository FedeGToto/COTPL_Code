using UnityEngine.Localization;

public abstract class TarotEffect
{
    public virtual void OnTarotAdded() { }
    public virtual void BattleStart() { }
    public virtual void BattleEnded() { }

    public virtual void OnTurnStart() { }
    public virtual void OnTurnEnd() { }

    public abstract string GetDescription(LocalizedString description, int level);
}
