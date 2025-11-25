using UnityEngine;
using static InputManager;

public class DialogueInput : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogues;

    private void OnEnable()
    {
        GameManager.Instance.Input.OnDialogueAdvance += AdvanceDialogue;

        EventManager.Instance.AddListener<StartDialogueEvent>(StartDialogue);
        EventManager.Instance.AddListener<FinishDialogueEvent>(EndDialogue);
    }

    private void OnDisable()
    {
        GameManager.Instance.Input.OnDialogueAdvance -= AdvanceDialogue;

        EventManager.Instance.RemoveListener<StartDialogueEvent>(StartDialogue);
        EventManager.Instance.RemoveListener<FinishDialogueEvent>(EndDialogue);
    }

    public void AdvanceDialogue()
    {
        dialogues.SubmitPressed();
    }

    private void StartDialogue(StartDialogueEvent e)
    {
        GameManager.Instance.Input.ChangeContext(InputContext.Dialogue);
    }

    public void EndDialogue(FinishDialogueEvent e)
    {
        GameManager.Instance.Input.ChangeContext(InputContext.Default);
    }
}
