using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerPrefab;
    private SpawnPoints[] _spawnPoints;
    private Pool<PlayerMovement> _pool;
    
    private void Awake()
    {
        _pool = new Pool<PlayerMovement>(_playerPrefab, 0, true);
        
        _spawnPoints = FindObjectsByType<SpawnPoints>(FindObjectsSortMode.None);
    }
    
    private void Start()
    {
        SpawnPoints startPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        var pointCreated = _pool.GetObject(startPoint.transform.position, startPoint.transform.rotation);
    }
}
