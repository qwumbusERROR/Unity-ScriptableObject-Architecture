using UnityEngine;
using UnityEngine.InputSystem;

public class UserInputService : MonoBehaviour
{
    [SerializeField] private ReactiveVariable<Vector2> _inputMove;
    [SerializeField] private ReactiveVariable<Vector2> _inputLook;
    
    private InputSystem_Actions _inputActions;
    
    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
    }
    private void OnEnable()
    {
        _inputActions.Enable();
        
        _inputActions.Player.Move.performed += OnMovePerformed;
        _inputActions.Player.Move.canceled += OnMoveCanceled;
        
        _inputActions.Player.Look.performed += OnLookPerformed;
        _inputActions.Player.Look.canceled += OnLookCanceled;
    }
    private void OnDisable()
    {
        _inputActions.Player.Move.performed -= OnMovePerformed;
        _inputActions.Player.Move.canceled -= OnMoveCanceled;
        
        _inputActions.Player.Look.performed -= OnLookPerformed;
        _inputActions.Player.Look.canceled -= OnLookCanceled;
        
        _inputActions.Disable();
    }
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _inputMove.Value = context.ReadValue<Vector2>();
    }
    
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _inputMove.Value = Vector2.zero;
    }
    
    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        _inputLook.Value = context.ReadValue<Vector2>();
    }
    
    private void OnLookCanceled(InputAction.CallbackContext context)
    {
        _inputLook.Value = Vector2.zero;
    }
}
