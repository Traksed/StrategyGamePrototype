using UnityEngine;

namespace UnitSystem
{
    public class UnitClick : MonoBehaviour
    {
        private UnityEngine.Camera _myCamera;
        public GameObject groundMarker;

        public LayerMask clickable;
        public LayerMask ground;

        private void Start()
        {
            _myCamera = UnityEngine.Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = _myCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
                {
                    UnitSelections.Instance.ClickSelect(hit.collider.gameObject);
                }
                else
                {
                    UnitSelections.Instance.DeselectAll();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                Ray ray = _myCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
                {
                    groundMarker.transform.position = hit.point + Vector3.up;
                    groundMarker.SetActive(false);
                    groundMarker.SetActive(true);
                }
            }
        }
    }
}
