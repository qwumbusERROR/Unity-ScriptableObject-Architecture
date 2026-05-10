using TMPro;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextViewer : MonoBehaviour
{
    [SerializeField] private ReactiveVariable<int> _variable;   
    [SerializeField] private float _animateDuration = 0.5f;
    private TextMeshProUGUI _text;
    private float _currentDisplayValue; 
    private Tween _runningTween;
    
    private void OnEnable()
    {
        _text = GetComponent<TextMeshProUGUI>();
        
        _variable?.AddListener(OnValueChanged);
        _currentDisplayValue = _variable.Value;
        
        UpdateText(_currentDisplayValue);
    }
    
    private void OnDisable()
    {
        _variable?.RemoveListener(OnValueChanged);
        _runningTween?.Kill();
    }
    
    private void OnValueChanged(int newValue)
    {
        _runningTween?.Kill();
        
        _runningTween = DOTween.To(() => _currentDisplayValue,x => {_currentDisplayValue = x; UpdateText(_currentDisplayValue);}, newValue, _animateDuration);
    }

    private void UpdateText(float value)
    {
        _text.text = Mathf.RoundToInt(value).ToString();
    }
}
