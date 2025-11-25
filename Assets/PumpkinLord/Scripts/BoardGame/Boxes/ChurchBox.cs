using Unity.Cinemachine;
using UnityEngine;

public class ChurchBox : BoxEffect
{
    [SerializeField] private int requiredSouls;
    [SerializeField] private string[] dialogues;

    [Header("Not Reached Objective")]
    [SerializeField] private CinemachineCamera dialogueCamera;
    [SerializeField] private DialogueTrigger notRequiredDialogue;

    public override void ApplyBoxEffect()
    {
        if (GameManager.Instance.Player.Souls >= requiredSouls)
        {
            EventManager.Instance.AddListener<FinishDialogueEvent>(OnDialogueEnded);

            int randomBoss = Random.Range(0, dialogues.Length);
            EventManager.Instance.TriggerEvent(new DialogueEnterEvent() { KnotName = dialogues[randomBoss] });

            dialogueCamera.gameObject.SetActive(true);
        }
        else
        {
            EventManager.Instance.AddListener<FinishDialogueEvent>(OnDialogueEnded);
            notRequiredDialogue.StartDialogue();
            dialogueCamera.gameObject.SetActive(true);
        }
    }

    private void OnDialogueEnded(FinishDialogueEvent e)
    {
        dialogueCamera.gameObject.SetActive(false);
    }
}
