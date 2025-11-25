using Ink.Runtime;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("Ink Story")]
    [SerializeField] private TextAsset inkJson;

    private Story story;
    private string currentKnot;
    private int currentChoiceIndex = -1;
    private bool dialoguePlaying = false;

    private InkExternalFunctions inkExternalFunctions;
    private InkDialogueVariable inkDialogueVariable;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";

    private void Awake()
    {
        story = new Story(inkJson.text);

        inkExternalFunctions = new();
        inkExternalFunctions.Bind(story);

        inkDialogueVariable = new InkDialogueVariable(story);
    }

    private void OnDestroy()
    {
        inkExternalFunctions.Unbind(story);
    }

    private void OnEnable()
    {
        EventManager.Instance.AddListener<DialogueEnterEvent>(EnterDialogue);
        EventManager.Instance.AddListener<UpdateChoiceEvent>(UpdateChoiceIndex);
        EventManager.Instance.AddListener<UpdateInkDialogueVariableEvent>(UpdateInkDialogueVariable);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener<DialogueEnterEvent>(EnterDialogue);
        EventManager.Instance.RemoveListener<UpdateChoiceEvent>(UpdateChoiceIndex);
        EventManager.Instance.RemoveListener<UpdateInkDialogueVariableEvent>(UpdateInkDialogueVariable);
    }

    private void UpdateInkDialogueVariable(UpdateInkDialogueVariableEvent e)
    {
        inkDialogueVariable.UpdateVariableState(e.Name, e.Value);
    }

    private void UpdateChoiceIndex(UpdateChoiceEvent e)
    {
        this.currentChoiceIndex = e.ChoiceIndex;
        ContinueOrExitStory();
    }

    public void SubmitPressed()
    {
        if (!dialoguePlaying) return;
        ContinueOrExitStory();
    }

    private void EnterDialogue(DialogueEnterEvent e)
    {
        if (dialoguePlaying) return;

        dialoguePlaying = true;

        EventManager.Instance.TriggerEvent(new StartDialogueEvent());

        if (!e.KnotName.Equals(""))
        {
            story.ChoosePathString(e.KnotName);
            currentKnot = e.KnotName;
        }
        else
        {
            Debug.LogWarning("Knot name was the empty string when entering dialogue.");
        }

        inkDialogueVariable.SyncVariablesAndStartListening(story);

        ContinueOrExitStory();
    }

    private void ContinueOrExitStory()
    {
        // make a choice, if applicable
        if (story.currentChoices.Count > 0 && currentChoiceIndex != -1)
        {
            story.ChooseChoiceIndex(currentChoiceIndex);
            story.Continue();
            currentChoiceIndex = -1;
        }

        if (story.canContinue)
        {
            string dialogueLine = story.Continue();
            while(IsLineBlank(dialogueLine) && story.canContinue)
            {
                dialogueLine = story.Continue();
            }

            if(IsLineBlank(dialogueLine) && !story.canContinue)
            {
                ExitDialogue();
            }
            else
            {
                string localizedLine = story.currentTags[0];
                var settings = HandleTags(story.currentTags);

                EventManager.Instance.TriggerEvent(new DisplayDialogueEvent()
                {
                    LocalizedLine = localizedLine,
                    Choices = story.currentChoices,
                    Settings = settings

                });
            }
        }
        else if (story.currentChoices.Count == 0)
        {
            ExitDialogue();
        }
    }

    private void ExitDialogue()
    {
        dialoguePlaying = false;

        EventManager.Instance.TriggerEvent(new FinishDialogueEvent() { CurrentKnot = currentKnot });

        inkDialogueVariable.StopListening(story);

        story.ResetState();
    }

    private DialogueSettings HandleTags(List<string> currentTags)
    {
        DialogueSettings settings = new DialogueSettings();
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogWarning("Tag could not be parsed");
                continue;
            }

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch(tagKey)
            {
                case SPEAKER_TAG:
                    settings.Speaker = tagValue;
                    break;
                case PORTRAIT_TAG:
                    settings.Portrait = tagValue;
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }

        }
        return settings;
    }

    private bool IsLineBlank(string dialogueLine)
    {
        return dialogueLine.Trim().Equals("") || dialogueLine.Trim().Equals("\n");
    }

    public struct DialogueSettings
    {
        public string Speaker;
        public string Portrait;
    }
}
