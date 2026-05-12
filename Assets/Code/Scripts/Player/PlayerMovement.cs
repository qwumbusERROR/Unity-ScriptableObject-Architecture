using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private ReactiveVariable<Vector2> _inputMove;
    [SerializeField] private ReactiveState _idleState;
    [SerializeField] private CharacterController _controller;
    [SerializeField, Range(1f, 10f)] private float _walkSpeed = 5f;
    [SerializeField, Range(0.1f, 1f)] private float _acceleration = 0.4f;  
    [SerializeField, Range(0.1f, 1f)] private float _deceleration = 0.25f; 
    [SerializeField, Range(0.1f, 3f)] private float _groundCheckDistance = 2f;
    [SerializeField] private float _gravityStrength = 9.81f;
    [SerializeField] private LayerMask _groundLayers = ~0;
    private Vector3 _currentHorizontalVelocity;
    private Vector3 _velocityGravity;
    private Vector3 _velocityRef;
    private Vector3 _groundNormal = Vector3.up;
    private bool _isGrounded = false;

    private void Update()
    {
        GroundChecker();

        Vector2 input = _inputMove.Value;
        Vector3 desiredDirection = input.x * transform.right + input.y * transform.forward;
        
        if(desiredDirection.magnitude == 0f)
        {
            if(_idleState != null)
            {
                _idleState.TryStart();
            }
        }
        else
        {
            _idleState.TryStop();
        }
        
        desiredDirection = ProjectPlane(desiredDirection).normalized;

        Vector3 targetHorizontalVelocity = desiredDirection * _walkSpeed;

        float smoothTime = input.sqrMagnitude > 0.01f ? _acceleration : _deceleration;

        _currentHorizontalVelocity = Vector3.SmoothDamp(_currentHorizontalVelocity,targetHorizontalVelocity,ref _velocityRef, smoothTime);
        
        if (_isGrounded)
        {
            if (_velocityGravity.y < 0)
                _velocityGravity.y = -0.5f; 
        }
        else
        {
            _velocityGravity.y -= _gravityStrength * Time.deltaTime;
        }
        Vector3 finalMove = _currentHorizontalVelocity * Time.deltaTime + new Vector3(0, _velocityGravity.y * Time.deltaTime, 0);

        _controller.Move(finalMove);
    }

    private void GroundChecker()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 
                _groundCheckDistance + 0.1f, _groundLayers))
        {
            _groundNormal = hit.normal;
            _isGrounded = true;
        }
        else
        {
            _groundNormal = Vector3.up;
            _isGrounded = false;
        }
    }

    private Vector3 ProjectPlane(Vector3 direction)
    {
        return direction - Vector3.Dot(direction, _groundNormal) * _groundNormal;
    }
}