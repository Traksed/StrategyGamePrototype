using UnityEngine;
using UnityEngine.InputSystem;

namespace InputActions
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float speed = 1;

        private CameraInput _cameraInput;
        private Vector2 _currentPosition;
        private Vector3 _touch;

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
            _cameraInput.Touch.TouchPress.started += StartTouch;
            _cameraInput.Touch.TouchPress.canceled += EndTouch;

        }

        private void StartTouch(InputAction.CallbackContext context)
        {
            _currentPosition = _cameraInput.Touch.TouchPosition.ReadValue<Vector2>();
        }
        
        private void EndTouch(InputAction.CallbackContext context)
        {
            var nextPosition = _cameraInput.Touch.TouchPosition.ReadValue<Vector2>();
            Vector2 deltaPosition = _currentPosition - nextPosition;
             Vector3 movePosition = new Vector3(transform.position.x + deltaPosition.x * speed * Time.deltaTime, transform.position.y, 
                 transform.position.z + deltaPosition.y * speed * Time.deltaTime);
             transform.position = movePosition;
        }
    }
}
