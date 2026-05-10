using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private ReactiveVariable<Vector2> _inputMove;  
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _groundCheckDistance = 2f;
    [SerializeField] private float _gravityStrength = 9.81f;
    [SerializeField] private LayerMask _groundLayers = ~0;
    [SerializeField] private CharacterController _controller;
    private Vector3 _groundNormal;
    private Vector3 _velocityGravity;
    private bool _isGrounded = false;
    private void Update()
    {      
        GroundChecker();
        
        Vector3 desiredVelocity = _inputMove.Value.x * transform.right + _inputMove.Value.y * transform.forward; 
        Vector3 horizontalMove = ProjectPlane(desiredVelocity) * _walkSpeed * Time.deltaTime;
        
        if (_isGrounded)
        {
            if (_velocityGravity.y < 0) _velocityGravity.y = -0.5f; 
            else _velocityGravity.y = 0f;
        }
        else
        {
            _velocityGravity.y -= _gravityStrength * Time.deltaTime;
        }
        
         Vector3 move = horizontalMove + new Vector3(0, _velocityGravity.y, 0) * Time.deltaTime;
        _controller.Move(move);
    }
    private void GroundChecker()
    {
        if (Physics.Raycast(transform.position,-transform.up, out RaycastHit hit, _groundCheckDistance + 0.1f, _groundLayers))
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
    private Vector3 ProjectPlane(Vector3 forward)
    {
        return forward - Vector3.Dot(forward, _groundNormal) * _groundNormal;
    }
}
