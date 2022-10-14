using UnityEngine;

namespace Camera
{
    public class CameraZooming : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        [SerializeField] private float radius = 5;
        [SerializeField] private Transform target;

        private Touch _touchStart;
        private Touch _touchEnd;
        private Vector3 _targetPos;

        private void Start()
        {
            if (target == null)
            {
                target = transform;
            }

            _targetPos = target.position;
        }

        void Update()
        {
            if (Input.touchCount == 2)
            {
                _touchStart = Input.GetTouch(0);
                _touchEnd = Input.GetTouch(1);

                Vector2 touchStartDeltaPos = _touchStart.position - _touchStart.deltaPosition;
                Vector2 touchEndDeltaPos = _touchEnd.position - _touchEnd.deltaPosition;

                float distDeltaTouches = (touchStartDeltaPos - touchEndDeltaPos).magnitude;
                float currentDistTouchesPos = (_touchStart.position - _touchEnd.position).magnitude;

                float distance = distDeltaTouches - currentDistTouchesPos;

                Zooming(distance);
            }
        }

        private void Zooming(float value)
        {
            float height = transform.position.y + (value * speed * Time.deltaTime);
            float delta = Mathf.Abs(height - _targetPos.y);

            if (delta <= radius)
                transform.position = new Vector3(transform.position.x, height, transform.position.z);
        }
    }
}