using UnityEngine;

public abstract class ObjectSceneInstaller : MonoBehaviour
{
    protected ActorWorld _world {get; private set;}
    private void Awake()
    {
        _world = FindFirstObjectByType<SceneBootstrapper>().World;
    }
}
