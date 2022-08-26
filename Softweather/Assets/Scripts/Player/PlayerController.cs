using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _cameraRoot;
    
    [Header("Movement")]
    [SerializeField] [Range(0f, 2)] private float _rotationSpeed = 1f;
    [SerializeField] private float _moveSpeed = 1f;

    [Space(10)]
    [SerializeField] private  float _jumpHeight = 1.2f;
    [SerializeField] private  float _gravity = -15.0f;
    
    [Header("Player Grounded")]
    [SerializeField] private bool _grounded = true;
    [SerializeField] private float _groundedOffset = -0.14f;
    [SerializeField] private float _groundedRadius = 0.5f;
    [SerializeField] private LayerMask _groundLayers;

    private CharacterController _controller;

    private Vector2 _input;
    private Vector2 _look;

    private float _rotationVelocity;
    private float _speed;
    private float _verticalVelocity;
    private float _xRotation;
    private bool _isJump;

    private const float THRESHOLD = 0.01f;
    private const float TERMINAL_VELOCITY = 53.0f;

    private void Awake()
    {
        TryGetComponent(out _controller);
    }

    private void Update()
    {
        Move();
        GroundedCheck();
        JumpAndGravity();
    }

    private void LateUpdate()
    {
        Rotation();
    }

    private void GroundedCheck()
    {
        var position = transform.position;
        var vector3 = new Vector3(position.x, position.y - _groundedOffset, position.z);
        Physics.Raycast(vector3, Vector3.down, out var hit, _groundedRadius, _groundLayers);
        _grounded = hit.collider != null;
    }

    private void Rotation()
    {
        if (_look.sqrMagnitude < THRESHOLD) return;
        
        var deltaTimeMultiplier = 1.0f;
        _rotationVelocity = _look.x * _rotationSpeed * deltaTimeMultiplier;
        
        var mouseY = _look.y * _rotationSpeed;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90, 90);
        
        _cameraRoot.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);

        transform.Rotate(Vector3.up * _rotationVelocity);
    }

    private void Move()
    {
        var inputDirection = new Vector3(_input.x, 0.0f, _input.y).normalized;
			
        if (_input != Vector2.zero)
        {
            inputDirection = transform.right * _input.x + transform.forward * _input.y;
        }
			
        _controller.Move(inputDirection * (_moveSpeed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }
    
    private void JumpAndGravity()
    {
        if (_grounded)
        {
            if (_verticalVelocity < 0.0f) _verticalVelocity = -2f;
            
            if (_isJump) 
                _verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }
        else _isJump = false;
        
        if (_verticalVelocity < TERMINAL_VELOCITY) 
            _verticalVelocity += _gravity * Time.deltaTime;
    }

    #region Input

    private void OnMove(InputValue value)
    {
        _input = value.Get<Vector2>();
    }
    
    private void OnLook(InputValue value)
    {
        _look = value.Get<Vector2>();
    }
    
    private void OnJump(InputValue value)
    {
        _isJump = value.isPressed;
    }

    #endregion
}
