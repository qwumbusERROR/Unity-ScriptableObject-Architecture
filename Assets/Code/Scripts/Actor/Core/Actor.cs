using System;
using System.Collections.Generic;

public class Actor 
{
    private readonly List<IInitialized> _inits = new();
    private readonly List<IUpdated> _updates = new();
    private readonly List<IDisposable> _disposes = new();
    
    public void AddBehaviour(object behaviour)
    {
        if (behaviour is IInitialized init)
            _inits.Add(init);
            
        if (behaviour is IUpdated update)
            _updates.Add(update);
            
        if (behaviour is IDisposable dispose)
            _disposes.Add(dispose);
    }
    public void InitializedActor()
    {
        foreach (var init in _inits)
            init.OnInitialized();
    }

    public void Disposes()
    {
        foreach (var dispose in _disposes)
            dispose.Dispose();
    }

    public void UpdatedActor(float deltaTime)
    {
        foreach (var update in _updates)
            update.OnUpdated(deltaTime);
    }
}
