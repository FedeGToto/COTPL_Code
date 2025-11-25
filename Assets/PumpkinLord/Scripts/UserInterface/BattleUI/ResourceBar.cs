using DG.Tweening;
using StatSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image fillBar;
    [SerializeField] private TextMeshProUGUI valueText;

    [Header("Animation")]
    [SerializeField] private float barDuration = 1f;

    private Resource resource;

    public void SetupBar(Resource resource)
    {
        this.resource = resource;
        resource.OnValueChanged += OnResourceChange;

        OnResourceChange(resource.Value);
    }

    private void OnDestroy()
    {
        resource.OnValueChanged -= OnResourceChange;
    }

    private void OnResourceChange(float resourceValue)
    {
        float resourceNormalized = 0;
       
        if (resourceValue > 0)
            resourceNormalized = resourceValue / resource.MaxValue;

        if (fillBar != null)
        {
            fillBar.DOFillAmount(resourceNormalized, barDuration);
        }

        if (valueText != null)
            valueText.DOCounter(int.Parse(valueText.text), (int)resourceValue, barDuration);
    }
}
