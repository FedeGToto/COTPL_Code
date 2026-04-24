using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGraphics : MonoBehaviour
{
    [SerializeField] private Toggle toggle;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;

    private void Reset()
    {
        toggle = GetComponent<Toggle>();
    }

    private void Awake()
    {
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
        OnToggleValueChanged(toggle.isOn);
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        text.color = isOn ? activeColor : inactiveColor;
    }
}
