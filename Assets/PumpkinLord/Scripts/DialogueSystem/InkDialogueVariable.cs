using Ink.Runtime;
using System.Collections.Generic;
using UnityEngine;

public class InkDialogueVariable
{
    private Dictionary<string, Ink.Runtime.Object> variables;

    public InkDialogueVariable(Story story)
    {
        variables = new Dictionary<string, Ink.Runtime.Object>();

        foreach (string name in story.variablesState)
        {
            var value = story.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
        }
    }

    public void SyncVariablesAndStartListening(Story story)
    {
        SyncVariablesToStory(story);
        story.variablesState.variableChangedEvent += UpdateVariableState;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= UpdateVariableState;
    }

    public void UpdateVariableState(string name, Ink.Runtime.Object value)
    {
        if (!variables.ContainsKey(name))
            return;
        variables[name] = value;
    }

    private void SyncVariablesToStory(Story story)
    {
        foreach (var variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }

}
