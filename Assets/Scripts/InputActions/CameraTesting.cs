using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace InputActions
{
    public class CameraTesting : MonoBehaviour
    {
        [SerializeField] private int speed = 1;
        
        private CameraInput _cameraInput;
        private Vector2 _currentPosition;
        private Touch _touch;

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
            //Touch.onFingerDown += OnFingerDown;
            Touch.onFingerMove += OnFingerMove;
        }

        private void OnFingerDown(Finger finger)
        {
            
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

        private void Update()
        {
            // if (_touch.valid)
            // {
            //     if (_touch.phase == TouchPhase.Moved)
            //     {
            //         Vector3 movePosition = new Vector3(transform.position.x + _touch.delta.x * speed * Time.deltaTime,
            //             transform.position.y,
            //             transform.position.z + _touch.delta.y * speed * Time.deltaTime);
            //         transform.position = movePosition;
            //     }
            // }
        }
    }
}
