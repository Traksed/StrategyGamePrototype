using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputActions
{
    public class CameraMover : MonoBehaviour
    {
        private CameraInput _cameraInput;

        private void Awake()
        {
            _cameraInput = new CameraInput();
            
        }

        private void OnEnable()
        {
            _cameraInput.Enable();
        }

        private void OnDisable()
        {
            _cameraInput.Disable();
        }
    }
}
