using UnityEngine;

namespace InputActions
{
    public class CameraMovement : MonoBehaviour
    {

        [SerializeField] private float speed = 1;
        
        private Touch _touch;
        
        private void Update()
        {
            if (Input.touchCount == 1)
            {
                _touch = Input.GetTouch(0);

                if (_touch.phase == TouchPhase.Moved)
                {
                    Vector3 movePos = new Vector3(
                        _touch.deltaPosition.x * speed * -1 * Time.deltaTime,
                        0,
                        _touch.deltaPosition.y * speed * -1 * Time.deltaTime);
                    
                    transform.position += movePos;
                }
            }
        }
    }
}