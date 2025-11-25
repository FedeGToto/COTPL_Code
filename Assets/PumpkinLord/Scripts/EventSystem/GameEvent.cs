using System.Collections.Generic;

public abstract class GameEvent
{
}

public class DiceThrownEvent : GameEvent
{
    
}

public class MoveEvent : GameEvent
{
    public int RemainingBoxes;
}

public class BoxLandEvent : GameEvent
{

}

public class OpenShopEvent : GameEvent
{

}

public class CloseShopEvent : GameEvent
{

}

public class StartBattleEvent : GameEvent
{
    public EnemySO Enemy;
}

public class BattleEndedEvent : GameEvent
{

}

public class MoneyChangeEvent : GameEvent
{
    public int Money;
}

public class SoulsChangeEvent : GameEvent
{
    public int Souls;
}

public class ItemAddedEvent : GameEvent
{
    public ItemSO Item;
    public int Quantity;
}

public class ChangeEquipEvent : GameEvent
{
    public ItemSO Equip;
}

// Dialogue Events
public class UpdateChoiceEvent : GameEvent
{
    public int ChoiceIndex;
}

public class UpdateInkDialogueVariableEvent : GameEvent
{
    public string Name;
    public Ink.Runtime.Object Value;
}

public class DisplayDialogueEvent : GameEvent
{
    public string LocalizedLine;
    public List<Ink.Runtime.Choice> Choices;
    public DialogueManager.DialogueSettings Settings;
}

public class DialogueEnterEvent : GameEvent
{
    public string KnotName;
}

public class StartDialogueEvent : GameEvent
{

}

public class FinishDialogueEvent : GameEvent
{
    public string CurrentKnot;
}