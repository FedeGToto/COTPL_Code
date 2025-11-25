using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    public State<T> currentState { get; private set; }
    public T owner;

    public StateMachine(T owner)
    {
        this.owner = owner;
        currentState = null;
    }

    public void ChangeState(State<T> newState)
    {
        currentState?.ExitState(owner);
        currentState = newState;
        currentState.EnterState(owner);
    }

    public void Update()
    {
        if (currentState != null)
            currentState.UpdateState(owner);
    }
}

public abstract class State<T>
{
    public abstract void EnterState(T owner);
    public abstract void ExitState(T owner);
    public abstract void UpdateState(T owner);
}