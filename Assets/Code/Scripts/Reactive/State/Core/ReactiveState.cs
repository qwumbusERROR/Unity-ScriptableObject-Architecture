using System;
using UnityEngine;

public abstract class ReactiveState : ScriptableObject 
{
    public bool IsActive {get; private set;}
    private event Action OnStarted;
    private event Action OnStopped;
    public Func<bool> CanStartCondition;
    public Func<bool> CanStopCondition;
    public bool TryStart()
    {
        if(IsActive) return false;
        
        bool canStart = CanStartCondition?.Invoke() ?? true;
        if (!canStart) return false;
        
        IsActive = true;
        
        OnStarted?.Invoke();
        return true;
    }
    public bool TryStop()
    {
        if(!IsActive) return false;
        
        bool canStop = CanStopCondition?.Invoke() ?? true;
        if (!canStop) return false;
        
        IsActive = false;
        
        OnStopped?.Invoke();
        return true;
    }

    public void ForceStart()
    {
        if (IsActive) return;
        IsActive = true;

        OnStarted?.Invoke();
    }
    public void ForceStop()
    {
        if (IsActive) return;
        IsActive = false;

        OnStopped?.Invoke();
    }

    public void AddListenerStart(Action action)
    {
        if (action == null) return;
        
        OnStarted += action;
    }
    public void RemoveListenerStart(Action action)
    {
        OnStarted -= action;
    }
    public void AddListenerStop(Action action)
    {
        if (action == null) return;
        
        OnStopped += action;
    }
    public void RemoveListenerStop(Action action)
    {
        OnStopped -= action;    
    }
} 



