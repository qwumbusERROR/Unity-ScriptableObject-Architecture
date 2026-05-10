using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ReactiveVariableListener<T> : MonoBehaviour
{
    [SerializeField] private List<ReactiveVariable<T>> _channelVariable = new List<ReactiveVariable<T>>();   
    public UnityEvent OnEvent;
    private void OnEnable()
    {
        foreach (var channel in _channelVariable)
        {
            channel.AddListener(Respond);
        }
    }
    private void OnDisable()
    {
        foreach (var channel in _channelVariable)
        {
            channel.RemoveListener(Respond);
        }
    }
    public abstract void Respond(T value);
}
