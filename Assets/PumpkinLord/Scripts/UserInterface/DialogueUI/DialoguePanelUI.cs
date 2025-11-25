using Ink.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class DialoguePanelUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject contentParent;

    [Header("DialogueTextes")]
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Portrait")]
    [SerializeField] private Image portraitImage;

    [Header("Choices")]
    [SerializeField] private DialogueChoiceButton dialogueChoiceTemplate;

    [Header("Localization")]
    [SerializeField] private string charactersTableName = "Characters";
    [SerializeField] private string dialoguesTableName = "Dialogues";

    [Header("Potraits")]
    [SerializeField] private Dictionary<string, Sprite> portraits;

    private void Awake()
    {
        contentParent.SetActive(false);
        ResetPanel();
    }

    private void OnEnable()
    {
        EventManager.Instance.AddListener<StartDialogueEvent>(DialogueStarted);
        EventManager.Instance.AddListener<DisplayDialogueEvent>(DisplayDialogue);
        EventManager.Instance.AddListener<FinishDialogueEvent>(DialogueFinished);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener<StartDialogueEvent>(DialogueStarted);
        EventManager.Instance.RemoveListener<DisplayDialogueEvent>(DisplayDialogue);
        EventManager.Instance.RemoveListener<FinishDialogueEvent>(DialogueFinished);
    }

    private void DialogueStarted(StartDialogueEvent e)
    {
        contentParent.SetActive(true);
    }

    private void DialogueFinished(FinishDialogueEvent e)
    {
        contentParent.SetActive(false);
        ResetPanel();
    }

    private void DisplayDialogue(DisplayDialogueEvent e)
    {
        GameManager.Instance.Input.ChangeContext(InputManager.InputContext.Dialogue);

        string localizedName = LocalizationSettings.StringDatabase.GetLocalizedString(charactersTableName, e.Settings.Speaker);
        string localizedString = LocalizationSettings.StringDatabase.GetLocalizedString(dialoguesTableName, e.LocalizedLine);

        Sprite portrait = GetPortrait(e.Settings.Portrait);

        SetupChoices(e.Choices);

        speakerText.text = localizedName;
        dialogueText.text = localizedString;
        SetPortrait(portrait);
    }

    private void SetupChoices(List<Choice> choices)
    {
        Transform instanceParent = dialogueChoiceTemplate.transform.parent;

        // Reset choices
        for (int i = 0; i < instanceParent.childCount; i++)
        {
            if (i == 0)
                continue;

            Destroy(instanceParent.GetChild(i).gameObject);
        }

        dialogueChoiceTemplate.gameObject.SetActive(true);

        Button firstSelection = null;
        for (int i = 0; i < choices.Count; i++)
        {
            DialogueChoiceButton choiceButton = Instantiate(dialogueChoiceTemplate, instanceParent);
            string localizedChoice = LocalizationSettings.StringDatabase.GetLocalizedString(dialoguesTableName, choices[i].tags[0]);

            choiceButton.SetChoiceText(localizedChoice);
            choiceButton.SetChoiceIndex(i);

            if (firstSelection == null)
                firstSelection = choiceButton.GetComponent<Button>();

            GameManager.Instance.Input.ChangeContext(InputManager.InputContext.UserInterface);
        }

        if (firstSelection != null)
            firstSelection.Select();
        dialogueChoiceTemplate.gameObject.SetActive(false);
    }

    private void ResetPanel()
    {
        dialogueText.text = "";
    }

    private Sprite GetPortrait(string portraitId = "none")
    {
        if (portraitId == "none" || string.IsNullOrEmpty(portraitId))
            return null;

        return portraits[portraitId];
    }

    private void SetPortrait(Sprite portrait = null)
    {
        portraitImage.gameObject.SetActive(portrait != null);

        if (portrait != null)
        {
            portraitImage.sprite = portrait;
        }
    }
}
