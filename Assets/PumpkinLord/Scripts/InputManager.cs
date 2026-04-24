using QFSW.QC;
using Rewired;
using System;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    [field: SerializeField] public InputContext Context { get; private set; }

    [SerializeField] private string dialogueAdvanceString = "DialogueAdvance";
    [SerializeField] private string pauseString = "Pause";
    [SerializeField] private string diceThrowString = "DiceThrown";

    private Player playerControls;
    private InputContext oldContext;

    // Dialogue Events
    public UnityAction OnDialogueAdvance;
    public UnityAction OnPause;
    public UnityAction OnDiceThrow;

    private void Awake()
    {
        playerControls = ReInput.players.GetPlayer(0);
    }

    private void Start()
    {
        QuantumConsole.Instance.OnActivate += Console_OnActivate;
        QuantumConsole.Instance.OnDeactivate += Console_OnDeactivate;
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

        if (playerControls.GetButtonDown(diceThrowString))
        {
            OnDiceThrow?.Invoke();
        }
    }

    public void ChangeContext(InputContext context)
    {
        if (context == this.Context)
            return;

        oldContext = this.Context;
        this.Context = context;
        SetMap(context.ToString());
    }

    public void SetOldContext()
    {
        ChangeContext(oldContext);
    }

    private void SetMap(string mapId)
    {
        Debug.Log("Changed input context to " + mapId);
        var ruleSet = playerControls.controllers.maps.mapEnabler.ruleSets.Find(item => item.tag == "context");

        foreach (var rule in ruleSet.rules)
        {
            rule.enable = false;
        }

        ruleSet.rules.Find(item => item.tag == mapId).enable = true;

        playerControls.controllers.maps.mapEnabler.Apply();
    }

    #region Quantum
    private void Console_OnDeactivate()
    {
        SetOldContext();
    }

    private void Console_OnActivate()
    {
        ChangeContext(InputContext.None);
    }
    #endregion

    public enum InputContext {None, Default, UserInterface, Dialogue }
}
