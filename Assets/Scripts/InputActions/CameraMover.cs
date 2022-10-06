using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputActions
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        
        private CameraInput _cameraInput;
        private Vector2 _direction;

        private void Awake()
        {
            _cameraInput = new CameraInput();
            _cameraInput.Camera.Move.performed += ctx => OnMove();

        }

        private void OnEnable()
        {
            _cameraInput.Enable();
        }

        private void OnDisable()
        {
            _cameraInput.Disable();
        }

        private void OnMove()
        {
            
        }
    }
}
