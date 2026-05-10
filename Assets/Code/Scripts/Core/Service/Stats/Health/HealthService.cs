using UnityEngine;

public abstract class HealthService : MonoBehaviour
{
    [SerializeField] protected ReactiveVariable<int> _healthValue;
    [SerializeField] protected ReactiveState _deadState;
    [SerializeField][Range(0, 1000)] private int _maxHealth = 100;

    private void OnEnable()
    {
        _deadState.CanStartCondition = () => _healthValue.Value <= 0;
    }
    
    public virtual void TakeDamage(int amount)
    {
        if (amount <= 0 || (_deadState != null && _deadState.IsActive) || _healthValue == null) return;
        
        int newHealth = _healthValue.Value - amount;
        _healthValue.Value = newHealth < 0 ? 0 : newHealth;
        
        if (_healthValue.Value <= 0 && !_deadState)
        {
            _deadState.TryStart();
        }   
    }
    
    public virtual void TakeHeal(int amount)
    {
        if (amount <= 0 || (_deadState != null && _deadState.IsActive) || _healthValue == null) return;
        
        int newHealth = _healthValue.Value + amount;
        
        _healthValue.Value = newHealth > _maxHealth ? _maxHealth : newHealth;
    }
    
}
