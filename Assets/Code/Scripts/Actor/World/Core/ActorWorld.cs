using System.Collections.Generic;
public class ActorWorld 
{
    private readonly List<Actor> _actors = new();
    private bool _isEnabled = false;
    public bool IsEnabled => _isEnabled;
    
    public void EnableWorld()
    {
        if (_isEnabled) return;
        
        _isEnabled = true;

        foreach (var actor in _actors)
            actor.InitializedActor();
    }
    public void AddActor(Actor actor)
    {
        _actors.Add(actor);

        if (_isEnabled)
            actor.InitializedActor();
    }
    public void RemoveActor(Actor actor)
    {
        _actors.Remove(actor);
    }
    public void DisposesWorld()
    {
        foreach (var actor in _actors)
            actor.Disposes();
    }
    public void UpdatedWorld(float deltaTime)
    {
        if (!_isEnabled) return;
        
        foreach (var actor in _actors)
            actor.UpdatedActor(deltaTime);
    }
}
