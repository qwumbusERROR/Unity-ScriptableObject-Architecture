using UnityEngine;

public class DamagerTest : MonoBehaviour
{
    [SerializeField] private HealthService _healthService;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _healthService.TakeDamage(10);
        }
    }
}
