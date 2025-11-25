using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ErrorUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button buttonTemplate;

    public void ShowMessage(string title, string message, params (string, UnityAction)[] actions)
    {
        gameObject.SetActive(true);
        titleText.text = title;
        descriptionText.text = message;

        bool selectedFirst = false;

        foreach (var action in actions)
        {
            Button newButton = Instantiate(buttonTemplate, buttonTemplate.transform.parent);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = action.Item1;
            newButton.onClick.AddListener(action.Item2);
            newButton.onClick.AddListener(HideMessage);

            if (!selectedFirst)
            {
                newButton.Select();
                selectedFirst = true;
            }
        }

        buttonTemplate.gameObject.SetActive(false);
    }

    public void HideMessage()
    {
        gameObject.SetActive(false);
    }
}
