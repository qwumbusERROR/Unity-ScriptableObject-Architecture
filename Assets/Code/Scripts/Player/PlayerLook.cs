using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private ReactiveState _gamePauseState;
    [SerializeField] private ReactiveVariable<Vector2> _mouseInput;
    [SerializeField] private float _sensitivity = 1f;
    [SerializeField] private bool _invert = false;
    [SerializeField] private Vector2 _lookLimits = new Vector2(-60f, 90f);
    [SerializeField, Range(0f, 30f)] private float _smoothSensitivity = 0.08f; 
    [SerializeField, Range(0, 20)]private int _smoothSteps = 10;
	[SerializeField, Range(0, 20)] private float _smoothWeight = 0.4f;
	[SerializeField] private Transform _playerRoot;
	[SerializeField] private Transform _cameraRoot;
    private float _currentSensitivity = 0f;
    private Vector2 _smoothMove;
    private Vector2 _lookAngle;
    private Vector2 _currentMouseLook;
    private List<Vector2> _smoothBuffer = new List<Vector2>();

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void LateUpdate()
    {
        if(_gamePauseState.IsActive) return;
        
        _currentSensitivity = Mathf.Lerp(_currentSensitivity, _sensitivity, Time.deltaTime * _smoothSensitivity);
        
        MoveView(_mouseInput.Value, Time.deltaTime);
    }
    
    private void MoveView(Vector2 lookInput, float time)
    {
        CalculateSmoothLookInput(lookInput, time);
        
        _lookAngle.x += _currentMouseLook.y * _currentSensitivity * (_invert ? 1f : -1f);
        _lookAngle.y += _currentMouseLook.x * _currentSensitivity;

        _lookAngle.x = ClampAngle(_lookAngle.x, _lookLimits.x, _lookLimits.y);
        
        _playerRoot.localRotation = Quaternion.Euler(0f, _lookAngle.y, 0f);
		_cameraRoot.localRotation = Quaternion.Euler(_lookAngle.x, 0f, 0f);
    }
    
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle > 360f)
            angle -= 360f;
        else if (angle < -360f)
            angle += 360f;

        return Mathf.Clamp(angle, min, max);
    }
    private void CalculateSmoothLookInput(Vector2 lookInput, float deltaTime)
    {
        if (deltaTime == 0f)
            return;

        _smoothMove = new Vector2(lookInput.x, lookInput.y);

        _smoothSteps = Mathf.Clamp(_smoothSteps, 1, 20);
        _smoothWeight = Mathf.Clamp01(_smoothWeight);

        while (_smoothBuffer.Count > _smoothSteps)
            _smoothBuffer.RemoveAt(0);

        _smoothBuffer.Add(_smoothMove);

        float weight = 1f;
        Vector2 average = Vector2.zero;
        float averageTotal = 0f;

        for (int i = _smoothBuffer.Count - 1; i > 0; i--)
        {
            average += _smoothBuffer[i] * weight;
            averageTotal += weight;
            weight *= _smoothWeight / (deltaTime * 60f);
        }

        averageTotal = Mathf.Max(1f, averageTotal);
        _currentMouseLook = average / averageTotal;
    }
}
