using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using TouchPhase = UnityEngine.TouchPhase;

namespace InputActions
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float speed;

        private CameraInput _cameraInput;
        private Touch _touch;
        private Vector3 _position;
        
        private void Awake()
        {
            _cameraInput = new CameraInput();
            _cameraInput.Enable();
            speed *= Time.deltaTime;
            _position = transform.position;
        }

        private void OnDisable()
        {
            _cameraInput.Disable();
        }

        private void Update()
        {
            _touch = _cameraInput.Camera.Move.ReadValue<Touch>();
            if (_cameraInput.Camera.Move.phase == (InputActionPhase)TouchPhase.Moved)
            {
                
                Vector3 movePos = new Vector3(
                    _position.x + _touch.deltaPosition.x * speed * -1,
                    transform.position.y,
                    _position.z + _touch.deltaPosition.y * speed * -1);

                Vector3 distance = movePos;
                
                transform.position = movePos;
            }
           
        }

        private void Move(Vector2 direction)
        {
            
        }
    }
}
