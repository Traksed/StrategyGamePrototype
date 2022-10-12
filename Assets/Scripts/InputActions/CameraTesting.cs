using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputActions
{
    public class CameraTesting : MonoBehaviour
    {
        [SerializeField] private int speed;
        private Camera _camera;
        private CameraInput _cameraInput;
        private Vector2 _currentPosition;
        
        private void Awake()
        {
            _camera = Camera.main;
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
            _cameraInput.Touch.Phase.performed += OnMoved;
            _cameraInput.Touch.TouchPress.started += StartTouch;
            _cameraInput.Touch.TouchPress.canceled += EndTouch;
        }

        private void StartTouch(InputAction.CallbackContext context)
        {
            _currentPosition = _cameraInput.Touch.TouchPosition.ReadValue<Vector2>();
        }

        private void EndTouch(InputAction.CallbackContext context)
        {
           
        }

        private  void OnMoved(InputAction.CallbackContext context)
        {
            var nextPosition = _cameraInput.Touch.TouchPosition.ReadValue<Vector2>();
            Vector2 deltaPosition = _currentPosition - nextPosition;
            Vector3 movePosition = new Vector3( deltaPosition.x * speed * Time.deltaTime, 0, 
                deltaPosition.y * speed * Time.deltaTime);
            //transform.DOMove(movePosition, 0.1f);
            _camera.transform.position += movePosition * Time.deltaTime*speed;
        }
    }
}
