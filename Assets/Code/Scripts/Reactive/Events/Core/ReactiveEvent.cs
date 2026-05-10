using System;
using UnityEngine;

public abstract class ReactiveEvent<T> : ScriptableObject 
{
    private event Action<T> OnUpdate;

    public void ForceStart(T value)
    {
        OnUpdate?.Invoke(value);
    }

    public void AddListener(Action<T> listener)
    {
        OnUpdate += listener;
    }

    public void RemoveListener(Action<T> listener)
    {
        OnUpdate -= listener;
    }
}
