using UnityEngine;

public class CameraTilt : MonoBehaviour
{
    [SerializeField] private Transform _playerRoot;    
    [SerializeField] private Transform _cameraRoot;    
    [SerializeField] private ReactiveState _gamePauseState; 
    [SerializeField] private float _tiltIntensity = 0.5f; 
    [SerializeField] private float _maxTiltAngle = 15f;    
    [SerializeField] private float _smoothTime = 0.15f;     
    private float _currentTilt = 0f;
    private float _targetTilt = 0f;
    private float _tiltVelocity = 0f;
    private float _lastYaw = 0f;
    private float _currentAngularVelocity = 0f;

    private void LateUpdate()
    {
        if (_gamePauseState.IsActive) return;
        
        float currentYaw = _playerRoot.eulerAngles.y;
        float deltaYaw = Mathf.DeltaAngle(_lastYaw, currentYaw);
        
        _currentAngularVelocity = deltaYaw / Time.deltaTime;
        _lastYaw = currentYaw;

        _targetTilt = Mathf.Clamp(_currentAngularVelocity * _tiltIntensity, -_maxTiltAngle, _maxTiltAngle);
        _currentTilt = Mathf.SmoothDamp(_currentTilt, _targetTilt, ref _tiltVelocity, _smoothTime);

        Vector3 currentEuler = _cameraRoot.localEulerAngles;
        _cameraRoot.localEulerAngles = new Vector3(currentEuler.x, currentEuler.y, _currentTilt);
    }
}