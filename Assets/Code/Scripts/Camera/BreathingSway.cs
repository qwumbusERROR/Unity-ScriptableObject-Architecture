using UnityEngine;

public class BreathingSway : MonoBehaviour
{
    [SerializeField] private ReactiveState _playerIdleState;
     [SerializeField] private ReactiveState _gamePauseState;
    [SerializeField] private Transform _target;
    [SerializeField] private AnimationCurve _breathingCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 0f);
    [SerializeField] private float _frequency = 0.25f;  
    [SerializeField] private float _amplitudeX = 0.02f;
    [SerializeField] private float _amplitudeY = 0.03f;
    [SerializeField] private float _phaseOffsetX = 0f;
    [SerializeField] private float _phaseOffsetY = 0.25f;
    [SerializeField] private float _tiltAmplitude = 1.5f;
    [SerializeField] private float _smoothTime = 0.3f;
    private Vector3 _originalPosition;
    private Quaternion _originalRotation;
    private float _currentIntensity;
    private float _targetIntensity;
    private float _intensityVelocity;

    private void Awake()
    {
        _originalPosition = _target.localPosition;
        _originalRotation = _target.localRotation;
    }
    private void OnEnable()
    {
        if (_playerIdleState != null)
        {
            _playerIdleState.AddListenerStart(OnIdleStart);
            _playerIdleState.AddListenerStop(OnIdleStop);
        }
        
        if (_playerIdleState.IsActive)
            _targetIntensity = 1f;
        else
            _targetIntensity = 0f;
        _currentIntensity = _targetIntensity;
    }

    private void OnDisable()
    {
        if (_playerIdleState != null)
        {
            _playerIdleState.RemoveListenerStart(OnIdleStart);
            _playerIdleState.RemoveListenerStop(OnIdleStop);
        }
        _target.localPosition = _originalPosition;
        _target.localRotation = _originalRotation;
    }

    private void OnIdleStart() => _targetIntensity = 1f;
    private void OnIdleStop()  => _targetIntensity = 0f;

    private void Update()
    {
        if(_gamePauseState.IsActive) return;
    
        _currentIntensity = Mathf.SmoothDamp(_currentIntensity, _targetIntensity, ref _intensityVelocity, _smoothTime);
        
        if (_currentIntensity <= 0.01f)
        {
            _target.localPosition = Vector3.Lerp(_target.localPosition, _originalPosition, Time.deltaTime / _smoothTime);
            _target.localRotation = Quaternion.Slerp(_target.localRotation, _originalRotation, Time.deltaTime / _smoothTime);
            return;
        }

        float time = Time.time * _frequency;

        float GetBreathValue(float phaseOffset)
        {
            float t = time + phaseOffset;
            t = t - Mathf.Floor(t);      
            float curveVal = _breathingCurve.Evaluate(t);
            return (curveVal - 0.5f) * 2f;
        }

        float breathX = GetBreathValue(_phaseOffsetX);
        float breathY = GetBreathValue(_phaseOffsetY);
        float breathTilt = GetBreathValue(_phaseOffsetX + 0.5f); 

        Vector3 offset = new Vector3(breathX * _amplitudeX,breathY * _amplitudeY,0f) * _currentIntensity;
        Quaternion tilt = Quaternion.Euler(0f, 0f, breathTilt * _tiltAmplitude * _currentIntensity);
        _target.localPosition = _originalPosition + offset;
        _target.localRotation = _originalRotation * tilt;
    }
}