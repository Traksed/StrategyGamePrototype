using UnityEngine;

namespace BuildingSystem3D
{
    //work
    public class ObjectDrag3D : MonoBehaviour
    {
        private Vector3 _offset;

        private void OnMouseDown()
        {
            _offset = transform.position - global::BuildingSystem3D.BuildingSystem3D.GetMouseWorldPosition();
        }

        private void OnMouseDrag()
        {
            Vector3 position = global::BuildingSystem3D.BuildingSystem3D.GetMouseWorldPosition() + _offset;
            transform.position = global::BuildingSystem3D.BuildingSystem3D.Current.SnapCoordinateToGrid(position);
        }
    }
}
