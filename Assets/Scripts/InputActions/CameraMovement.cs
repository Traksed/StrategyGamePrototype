using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private float speed = 1;
    [SerializeField] private float radius = 10;
    [SerializeField] private Transform target;

    private Touch _touch;
    private Vector3 _targetPos;

    private void Start()
    {
        if (target == null)
        {
            target = transform;
        }

        _targetPos = target.position;
    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Moved)
            {
                Vector3 movePos = new Vector3(
                    transform.position.x + _touch.deltaPosition.x * speed * -1 * Time.deltaTime,
                    transform.position.y,
                    transform.position.z + _touch.deltaPosition.y * speed * -1 * Time.deltaTime);

                Vector3 distance = movePos - _targetPos;

                if (distance.magnitude < radius)
                    transform.position = movePos;
            }
        }
    }
}