using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReactiveVariable<T> : ScriptableObject
{
    [SerializeField] protected T _value;
    public T Value
    {
        get => _value;
        
        set
        {
            if (!EqualityComparer<T>.Default.Equals(_value, value))
            {
                _value = value;
                OnUpdate?.Invoke(value);
            }
        }
    }
    private event Action<T> OnUpdate;
    public void AddListener(Action<T> action)
    {   
        if(action == null) return;
        
        OnUpdate += action;
    } 
    public void RemoveListener(Action<T> action)
    {
        OnUpdate -= action;
    }
    public void ForceStart()
    {
        OnUpdate?.Invoke(_value);
    }
}
