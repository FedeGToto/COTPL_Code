using UnityEngine;

[System.Serializable]
public class MagicItem : ItemEffect
{
    [field: SerializeReference] public Spell Spell;
}
