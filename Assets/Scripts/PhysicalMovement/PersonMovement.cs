using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PersonMovement : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _playerJumpForce;
    [SerializeField] private ForceMode _appliedForceMode;

    private float _currentSpeed;
    private float _xAxis;
    private float _zAxis;
    private Rigidbody _rb;
    private RaycastHit _hit;
    private Vector3 _groundLocation;
    private bool _isShiftPressedDown;
    private bool _playerIsJumping;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _xAxis = Input.GetAxis("Horizontal");
        _zAxis = Input.GetAxis("Vertical");

        _currentSpeed = _isShiftPressedDown ? _runSpeed : _walkSpeed;

        _playerIsJumping = Input.GetButton("Jump");

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out _hit, Mathf.Infinity))
        {
            if (_hit.collider.CompareTag("Ground"))
                _groundLocation = _hit.point;

            var distanceFromPlayerToGround = Vector3.Distance(transform.position, _groundLocation);

            if (distanceFromPlayerToGround > 0.5f)
                _playerIsJumping = false;
        }
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(transform.position + Time.deltaTime * _currentSpeed * transform.TransformDirection(_xAxis, 0f, _zAxis));

        if (_playerIsJumping)
            Jump(_playerJumpForce, _appliedForceMode);
    }

    private void OnGUI()
    {
        _isShiftPressedDown = Event.current.shift;
    }

    private void Jump(float jumpForce, ForceMode forceMode)
    {
        _rb.AddForce(jumpForce * _rb.mass * Time.deltaTime * Vector3.up * 10, forceMode);
    }
}
