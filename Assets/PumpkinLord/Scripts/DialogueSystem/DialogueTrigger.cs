using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private string dialogueKnotName;

    public void StartDialogue()
    {
        if (dialogueKnotName != "")
        {
            EventManager.Instance.TriggerEvent(new DialogueEnterEvent() { KnotName = dialogueKnotName });
        }
        else
        {
            Debug.LogError("A dialogues has not been setted");
        }
    }
}
