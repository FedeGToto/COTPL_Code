using System;
using Unity.Cinemachine;
using UnityEngine;

public class HorseBox : BoxEffect
{
    [SerializeField] private DialogueTrigger dialogue;
    [SerializeField] private CinemachineCamera dialogueCamera;

    public override void ApplyBoxEffect()
    {
        EventManager.Instance.AddListener<FinishDialogueEvent>(OnDialogueEnded);
        dialogue.StartDialogue();
        dialogueCamera.gameObject.SetActive(true);
    }

    private void OnDialogueEnded(FinishDialogueEvent e)
    {
        dialogueCamera.gameObject.SetActive(false);
    }
}
