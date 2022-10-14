using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace InputActions
{
    public class CameraTesting : MonoBehaviour
    {
        [SerializeField] private int speed = 1;
        
        private CameraInput _cameraInput;
        private Vector2 _currentPosition;
        private Camera _camera;

        private void Awake()
        {
            _cameraInput = new CameraInput();
            _camera= Camera.main;
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

        private void Update()
        {
            
        }


        private void StartTouch(InputAction.CallbackContext context)
        {
            _currentPosition = _cameraInput.Touch.TouchPosition.ReadValue<Vector2>() ;
        }

        private void EndTouch(InputAction.CallbackContext context)
        {
            
        }

        private  void OnMoved(InputAction.CallbackContext context)
        {
            // var nextPosition = _cameraInput.Touch.TouchPosition.ReadValue<Vector2>();
            // Vector2 deltaPosition = _currentPosition - nextPosition;
            // Vector3 movePosition = new Vector3( deltaPosition.x * Time.deltaTime, 0, 
            //      deltaPosition.y * Time.deltaTime);
            // //transform.DOMove(movePosition, 0.1f);
            // transform.position += movePosition * Time.deltaTime * speed;

            var moveVector = _cameraInput.Touch.TouchPosition.ReadValue<Vector2>() ;
            var delta = _currentPosition - moveVector;
            var direction = new Vector3(
                delta.x* speed * Time.deltaTime,
                transform.position.y,
                delta.y* Time.deltaTime * speed);
            transform.position = direction;
        }
    }
}
