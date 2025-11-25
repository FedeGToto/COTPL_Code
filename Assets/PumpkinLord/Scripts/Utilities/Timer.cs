using System;
using UnityEngine;

public class Timer
{
    public bool IsActive {  get; private set; }
    public float Value { get; private set; }
    public float MaxValue { get; private set; }

    public Action OnTimerStart;
    public Action OnTimerUpdate;
    public Action OnTimerPause;
    public Action OnTimerStop;

    public Timer(float value)
    {
        this.MaxValue = value;
    }

    public void Start()
    {
        IsActive = true;
        OnTimerStart?.Invoke();
    }

    public void Pause()
    {
        IsActive = false;
        OnTimerPause?.Invoke();
    }

    public void Stop()
    {
        IsActive = false;
        Value = 0f;
        OnTimerStop?.Invoke();
    }

    public void Update()
    {
        if (IsActive)
        {
            if (Value >= MaxValue)
            {
                Stop();
                return;
            }

            Value += Time.deltaTime;
            OnTimerUpdate?.Invoke();
        }
    }
}
