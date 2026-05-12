using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Slider))]
public class SliderViewer : MonoBehaviour
{
    [SerializeField] private ReactiveVariable<int> _variable;
    [SerializeField] private float _animateDuration = 0.5f;
    private int _minValue = 0;
    private int _maxValue;
    private Slider _slider;
    private Tween _runningTween;
    
     private void OnEnable()
    {
        _slider = GetComponent<Slider>();
        
        _slider.minValue = _minValue;
        
        _maxValue = _variable.Value;
        _slider.maxValue = _maxValue;
        
        if (_variable != null)
        {
            _variable.AddListener(OnValueChanged);
            _slider.value = _variable.Value; 
        }
    }
    private void OnDisable()
    {
        if (_variable != null)
            _variable.RemoveListener(OnValueChanged);
        
        _runningTween?.Kill();
    }

    private void OnValueChanged(int newValue)
    {
        _runningTween?.Kill();
        _runningTween = _slider.DOValue(newValue, _animateDuration);
    }
}

