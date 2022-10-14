using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace Camera
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private int speed = 1;

        private void OnEnable() 
        { 
            EnhancedTouchSupport.Enable();
        }

        private void OnDisable() 
        {
            EnhancedTouchSupport.Disable();
        }

        private void Update()
        {
            foreach (var touch in Touch.activeTouches)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    Vector3 movePos = new Vector3(
                        touch.delta.x * speed * -1 * Time.deltaTime,
                        0,
                        touch.delta.y * speed * -1 * Time.deltaTime);
                    
                    transform.position += movePos;
                }
            }
        }
    }
}
