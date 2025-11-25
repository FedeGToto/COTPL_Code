using System;
using TMPro;
using UnityEngine;

public class StatUI : MonoBehaviour
{
    public Character Char;

    [SerializeField] private string statId;
    [SerializeField] private TextMeshProUGUI value;

    private void Start()
    {
        if (Char == null)
        {
            Debug.LogError("A reference Character has not been assigned.");
            return;
        }

        Char.Stats[statId].ValueChanged += UpdateValue;
        Char.Stats[statId].ModifiersChanged += UpdateValue;

        UpdateValue();
    }

    private void OnDestroy()
    {
        Char.Stats[statId].ValueChanged -= UpdateValue;
        Char.Stats[statId].ModifiersChanged -= UpdateValue;
    }

    private void UpdateValue()
    {
        value.text = Char.Stats[statId].Value.ToString();
    }
}
