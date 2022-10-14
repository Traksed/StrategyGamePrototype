using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace InputActions
{
    public class CameraTesting : MonoBehaviour
    {
        [SerializeField] private int speed = 1;
        
        private CameraInput _cameraInput;

        private void Awake()
        {
            _cameraInput = new CameraInput();
        }

        private void OnEnable() 
        { 
            _cameraInput.Enable(); 
            EnhancedTouchSupport.Enable();
        }

        private void OnDisable() 
        {
            _cameraInput.Disable();
            EnhancedTouchSupport.Disable();
        }

        private void Start()
        {
            Touch.onFingerMove += OnFingerMove;
        }

        private void OnFingerMove(Finger finger)
        {
            if (finger.currentTouch.phase == TouchPhase.Moved)
            {
                Vector3 movePosition = new Vector3(
                    finger.currentTouch.delta.x, 
                    0, 
                    finger.currentTouch.delta.y );
                transform.position += movePosition * speed * Time.deltaTime * -1;
            }
        }
    }
}
