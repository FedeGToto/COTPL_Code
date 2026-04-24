using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeightedOption<T>
{
    public T option;
    public float weight = 1f;
}

public class WeightedRandomSelector<T>
{
    private List<WeightedOption<T>> options = new();
    private float totalWeight = 0f;

    public void AddOption(T option, float weight)
    {
        options.Add(new WeightedOption<T> { option = option, weight = weight });
        totalWeight += weight;
    }

    public void RemoveOption(T option)
    {
        for (int i = options.Count - 1; i >= 0; i--)
        {
            if (EqualityComparer<T>.Default.Equals(options[i].option, option))
            {
                totalWeight -= options[i].weight;
                options.RemoveAt(i);
            }
        }
    }

    public T SelectRandom()
    {
        if (options.Count == 0) return default(T);

        float randomValue = Random.Range(0f, totalWeight);
        float currentWeight = 0f;

        foreach (var weightedOption in options)
        {
            currentWeight += weightedOption.weight;
            if (randomValue <= currentWeight)
                return weightedOption.option;
        }

        return options[options.Count - 1].option;
    }
}