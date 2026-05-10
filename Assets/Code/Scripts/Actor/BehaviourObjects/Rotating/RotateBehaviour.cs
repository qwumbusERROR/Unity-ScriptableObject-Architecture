using System;
using UnityEngine;

public class RotateBehaviour : IInitialized, IUpdated, IDisposable
{
    private readonly Transform _transform;
    private readonly ReactiveVariable<Vector3> _rotateVelocity;

    public RotateBehaviour(Transform transform, ReactiveVariable<Vector3> rotation)
    {
        _transform = transform;
        _rotateVelocity = rotation;
    }

    public void Dispose()
    {

    }

    public void OnInitialized()
    {
        
    }

    public void OnUpdated(float time)
    {
        _transform.Rotate(_rotateVelocity.Value * time);
    }
}
