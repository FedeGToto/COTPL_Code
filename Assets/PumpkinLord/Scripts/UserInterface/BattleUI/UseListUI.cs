using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UseListUI : MonoBehaviour
{
    [SerializeField] private UsableUI buttonTemplate;
    [SerializeField] private Transform buttonParent;

    [Header("Description")]
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void OpenList(List<ItemSO> items)
    {
        buttonTemplate.gameObject.SetActive(true);
        for (int i = 0; i < buttonParent.childCount; i++)
        {
            if (i == 0) continue;

            Destroy(buttonParent.GetChild(i).gameObject);
        }

        bool isSelectedFirst = false;

        foreach (ItemSO item in items)
        {
            UsableUI buttonInstance = Instantiate(buttonTemplate, buttonParent);
            buttonInstance.Setup(item);

            if (!isSelectedFirst)
            {
                buttonInstance.GetComponent<Button>().Select();
                buttonInstance.UpdateDescription();
                isSelectedFirst = true;
            }
        }

        buttonTemplate.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public void UpdateDescription(string description)
    {
        descriptionText.text = description;
    }
}
