using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    [SerializeField] private float _mouseSensivity;
    [SerializeField] private Transform _player;

    private float _xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        _player.Rotate(Vector3.up * mouseX);
    }

}
