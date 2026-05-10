using UnityEngine;

public class HelloWorldSender : MonoBehaviour
{
    [SerializeField] private ReactiveVariable<string> _channel;    
    public void Send()
    {
        _channel.ForceStart();
    }
    
    public void Welcome()
    {
        Debug.Log("Welcome");
    }
}
