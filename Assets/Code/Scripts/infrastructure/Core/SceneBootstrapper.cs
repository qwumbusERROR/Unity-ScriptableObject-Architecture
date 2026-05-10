using UnityEngine;

public abstract class SceneBootstrapper : MonoBehaviour
{
    private ActorWorld _world;
    public ActorWorld World => _world;
    
    private void Awake()
    {
        _world = new ActorWorld();

        Install();
        _world.EnableWorld();
    }
    
    public virtual void Install()
    {
        
    }
    private void Update()
    {
        float time = Time.deltaTime;
        _world.UpdatedWorld(time);
    }
    
    private void OnDisable()
    {
        _world.DisposesWorld();
    }
}
