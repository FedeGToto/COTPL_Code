public abstract class ZoneEffect
{
    public virtual void OnTurnStart(Unit unit) { }
    public virtual void OnTurnEnd(Unit unit) { }

    public virtual void OpenZone() { }
    public virtual void CloseZone() { }
}
