using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 _startPos;
    private float _deltaX, _deltaY;
    
    void Start()
    {
        _startPos = Input.mousePosition;
        _startPos = UnityEngine.Camera.main.ScreenToWorldPoint(_startPos);

        _deltaX = _startPos.x - transform.position.x;
        _deltaY = _startPos.y - transform.position.y;
    }
    
    void Update()
    {
        Vector3 mousePos = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 pos = new Vector3(mousePos.x - _deltaX, mousePos.y - _deltaY);

        Vector3Int cellPos = BuildingSystem.current.gridLayout.WorldToCell(pos);
        transform.position = BuildingSystem.current.gridLayout.CellToLocalInterpolated(cellPos);
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonUp(0))
        {
            gameObject.GetComponent<PlaceableObject>().CheckPlacement();
            Destroy(this);
        }
    }
}
