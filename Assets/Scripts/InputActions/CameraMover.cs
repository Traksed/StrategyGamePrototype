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

        private void Start()
        {
            _cameraInput.Touch.TouchPress.started += stx => StartMove(stx);
            _cameraInput.Touch.TouchPress.canceled += stx => EndMove(stx);
        }

        private void StartMove(InputAction.CallbackContext context)
        {
            Debug.Log("Инфа по началу тача: " + _cameraInput.Touch.TouchPosition.ReadValue<Vector2>());
        }
        
        private void EndMove(InputAction.CallbackContext context)
        {
            Debug.Log("Инфа по концу тача: " + _cameraInput.Touch.TouchPosition.ReadValue<Vector2>());
        }
    }
}
