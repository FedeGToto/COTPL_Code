using QFSW.QC;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TarotsManager : MonoBehaviour
{
    public static TarotsManager Instance;

    [SerializeField] private TarotSO[] tarots;
    [SerializeField] private int maxTarotLevel = 5;

    [SerializeField] private int tarotsPerDrawn = 3;

    [SerializeField] private TarotSelectorUI selectorUI;

    private List<TarotSO> instancedTarots = new();
    private Dictionary<string, int> currentTarots = new();

    private void Awake()
    {
        Instance = this;
    }

    [Command("draw-tarot")]
    public void DrawTarot()
    {
        TarotSO[] selectedTarots = GetPossibleTarots(); 
        selectorUI.OpenShop(selectedTarots);
    }

    public void PickTarot(TarotSO tarot)
    {
        string tarotID = tarot.ID;

        if (!currentTarots.ContainsKey(tarotID))
        {
            var instancedTarot = Instantiate(tarot);
            instancedTarots.Add(instancedTarot);
            currentTarots.Add(tarotID, 0);
        }

        TarotSO tar = GetTarotByID(tarotID);

        if (currentTarots[tarotID] < maxTarotLevel)
            currentTarots[tarotID]++;

        // Apply effects
        tar.Effect.OnTarotAdded();

        EventManager.Instance.TriggerEvent(new PickedTarotEvent { Tarot = tar });
    }

    public TarotSO GetTarotByID(string ID)
    {
        foreach (var tarot in instancedTarots)
        {
            if (tarot.ID == ID)
                return tarot;
        }

        return null;
    }

    public bool HasTarot(string ID) => currentTarots.ContainsKey(ID);

    public int GetLevel(string ID)
    {
        if (currentTarots.ContainsKey(ID))
            return currentTarots[ID];
        else return 0;
    }

    public int GetLevel(TarotSO tarot)
    {
        return GetLevel(tarot.ID);
    }

    public IEnumerable<KeyValuePair<TarotSO, int>> GetTarotInventory()
    {
        foreach(var tarot in instancedTarots)
        {
            yield return new KeyValuePair<TarotSO, int>(tarot, GetLevel(tarot.ID));
        }
    }

    private TarotSO[] GetPossibleTarots()
    {
        List<TarotSO> possibleTarots = new();

        possibleTarots.AddRange(tarots);

        RemoveMaxLevelTarots(ref possibleTarots);
        return ExtractTarots(ref possibleTarots).ToArray();
    }

    private List<TarotSO> ExtractTarots(ref List<TarotSO> tarots)
    {
        List<TarotSO> result = new();
        int iterations = Mathf.Min(tarotsPerDrawn, tarots.Count);

        for (int i = 0; i < iterations; i++)
        {
            int randomId = Random.Range(0, tarots.Count);
            result.Add(tarots[randomId]);
            tarots.RemoveAt(randomId);
        }

        return result;
    }

    private void RemoveMaxLevelTarots(ref List<TarotSO> tarots)
    {
        List<string> tarotsToRemove = new();

        foreach(var tarot in currentTarots)
        {
            Debug.Log(tarot.Key + " LV. " + tarot.Value);
            if (tarot.Value == maxTarotLevel)
                tarotsToRemove.Add(tarot.Key);
        }

        foreach (var tarotToRemove in tarotsToRemove)
        {
            tarots.Remove(tarots.Find(x => x.ID == tarotToRemove));
        }
    }

    public void StartCombat()
    {
        foreach(var tarot in instancedTarots)
        {
            tarot.Effect.BattleStart();
        }    
    }
    public void EndCombat()
    {
        foreach (var tarot in instancedTarots)
        {
            tarot.Effect.BattleEnded();
        }
    }
    public void CheckStartTurn()
    {
        foreach (var tarot in instancedTarots)
        {
            tarot.Effect.OnTurnStart();
        }
    }
    public void CheckEndTurn()
    {
        foreach (var tarot in instancedTarots)
        {
            tarot.Effect.OnTurnEnd();
        }
    }
}
