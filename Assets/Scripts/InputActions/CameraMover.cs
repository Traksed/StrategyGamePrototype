using UnityEngine;
using UnityEngine.InputSystem;

namespace InputActions
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        [SerializeField] private float radius = 10;

        private CameraInput _cameraInput;
        private Vector2 _currentPosition;

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
            _cameraInput.Touch.TouchPress.started += StartMove;
            _cameraInput.Touch.TouchPress.canceled += EndMove;

        }

        private void StartMove(InputAction.CallbackContext context)
        {
            _currentPosition = _cameraInput.Touch.TouchPosition.ReadValue<Vector2>();
            Debug.Log("Current position: " + _currentPosition);
        }
        
        private void EndMove(InputAction.CallbackContext context)
        {
            var nextPosition = _cameraInput.Touch.TouchPosition.ReadValue<Vector2>();
            Vector2 deltaPosition = new Vector2(nextPosition.x - _currentPosition.x, nextPosition.y - _currentPosition.y);
            Vector3 movePosition = new Vector3(transform.position.x + deltaPosition.x * speed * Time.deltaTime * -1, transform.position.y, 
                transform.position.z + deltaPosition.y * speed * Time.deltaTime * -1);
            transform.position = movePosition;
        }
    }
}
