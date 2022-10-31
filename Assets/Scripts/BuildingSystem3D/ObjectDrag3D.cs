using Camera;
using UnityEngine;

namespace BuildingSystem3D
{
    //work
    public class ObjectDrag3D : MonoBehaviour
    {
        private Vector3 _offset;
        private CameraMover _cameraMover;
        private UnitDrag _unitDrag;

        private void Awake()
        {
            _cameraMover = UnityEngine.Camera.main.GetComponent<CameraMover>();
            _unitDrag = (UnitDrag)FindObjectOfType(typeof(UnitDrag));
        }

        private void OnMouseDown()
        {
            _cameraMover.enabled = false;
            _unitDrag.enabled = false;
            _offset = transform.position - global::BuildingSystem3D.BuildingSystem3D.GetMouseWorldPosition();
        }

        private void OnMouseDrag()
        {
            Vector3 position = global::BuildingSystem3D.BuildingSystem3D.GetMouseWorldPosition() + _offset;
            transform.position = global::BuildingSystem3D.BuildingSystem3D.Current.SnapCoordinateToGrid(position);
        }

        private void OnMouseUp()
        {
            _cameraMover.enabled = true;
            _unitDrag.enabled = true;
        }
    }
}
