using UnityEngine;

public class PlayerHealthListeners : MonoBehaviour
{
    [SerializeField] private ReactiveEvent<int> _channel;
    [SerializeField] private HealthService _healthService;    
    private void OnEnable()
    {
        _channel?.AddListener(DamageReceiver);
    }

    private void OnDisable()
    {
        _channel?.RemoveListener(DamageReceiver);
    }
    public void DamageReceiver(int amount)
    {
        _healthService.TakeDamage(amount);
    }
}
