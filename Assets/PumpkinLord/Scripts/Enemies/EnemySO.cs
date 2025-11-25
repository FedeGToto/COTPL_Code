using System;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Pumpkin Lord/Characters/Enemy")]
public class EnemySO : ScriptableObject
{
    [SerializeField] private LocalizedString enemyName;

    [field: SerializeField] public Enemy Enemy { get; protected set; }
    [field: SerializeField] public BaseStatsSO BaseStats { get; protected set; }

    public string GetName() => enemyName.GetLocalizedString();
}
