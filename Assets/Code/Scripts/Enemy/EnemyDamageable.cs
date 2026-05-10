using UnityEngine;

public class EnemyDamageable : MonoBehaviour
{
    [SerializeField] private ReactiveEvent<int> _damageChannel;
    [SerializeField] private int _damage = 10;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _damageChannel.ForceStart(_damage);
        }
    }
}
