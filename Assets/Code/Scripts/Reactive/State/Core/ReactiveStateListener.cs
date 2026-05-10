using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ReactiveStateListener : MonoBehaviour
{
    [SerializeField] private List<ReactiveState> _channelStates;
    
    public UnityEvent OnStartChange;
    public UnityEvent OnStopChange;

    private void OnEnable()
    {
        foreach (var state in _channelStates)
        {
            state.AddListenerStart(Respond);
            state.AddListenerStop(Respond);
        }
    }
    
    private void OnDisable()
    {
        foreach (var state in _channelStates)
        {
            state.RemoveListenerStart(Respond);
            state.RemoveListenerStop(Respond);
        }
    }
    
    public abstract void Respond();
}
