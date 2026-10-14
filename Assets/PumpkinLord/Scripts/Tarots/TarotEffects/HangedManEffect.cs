
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

[System.Serializable]
public class HangedManEffect : TarotEffect
{
    private int hanged;

    public override void OnTarotAdded()
    {
        base.OnTarotAdded();

        hanged++;
    }

    public bool HasStack() => hanged > 0;

    public void UseStack()
    {
        hanged--;
    }

    public override string GetDescription(LocalizedString description, int level)
    {
        (description["value"] as IntVariable).Value = hanged;
        return description.GetLocalizedString();
    }
}
