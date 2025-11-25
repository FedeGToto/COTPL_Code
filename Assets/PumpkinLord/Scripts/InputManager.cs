using Rewired;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    [field: SerializeField] public InputContext Context { get; private set; }

    [SerializeField] private string dialogueAdvanceString = "DialogueAdvance";
    [SerializeField] private string pauseString = "Pause";

    private Player playerControls;

    // Dialogue Events
    public UnityAction OnDialogueAdvance;
    public UnityAction OnPause;

    private void Awake()
    {
        playerControls = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
        if (playerControls.GetButtonDown(dialogueAdvanceString))
        {
            OnDialogueAdvance?.Invoke();
        }

        if (playerControls.GetButtonDown(pauseString))
        {
            OnPause?.Invoke();
        }
    }

    public void ChangeContext(InputContext context)
    {
        if (context == this.Context)
            return;

        this.Context = context;
        SetMap(context.ToString());
    }

    private void SetMap(string mapId)
    {
        Debug.Log(mapId);
        var ruleSet = playerControls.controllers.maps.mapEnabler.ruleSets.Find(item => item.tag == "context");

        foreach (var rule in ruleSet.rules)
        {
            rule.enable = false;
        }

        ruleSet.rules.Find(item => item.tag == mapId).enable = true;

        playerControls.controllers.maps.mapEnabler.Apply();
    }

    public enum InputContext {None, Default, UserInterface, Dialogue }
}
