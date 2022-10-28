using UnityEngine;

public class UnitClick : MonoBehaviour
{
    private UnityEngine.Camera _myCam;
    public GameObject GroundMarker;

    public LayerMask clickable;
    public LayerMask ground;

    private void Start()
    {
        _myCam = UnityEngine.Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = _myCam.ScreenPointToRay(Input.mousePosition);

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
            Ray ray = _myCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                GroundMarker.transform.position = hit.point;
                GroundMarker.SetActive(false);
                GroundMarker.SetActive(true);
            }
        }
    }
}
