using UnityEngine;

    public class ObjectDrag3D : MonoBehaviour
    {
        private Vector3 _offset;

        private void OnMouseDown()
        {
            _offset = transform.position - BuildingSystem3D.GetMouseWorldPosition();
        }

        private void OnMouseDrag()
        {
            Vector3 position = BuildingSystem3D.GetMouseWorldPosition() + _offset;
            transform.position = BuildingSystem3D.Current.SnapCoordinateToGrid(position);
        }
}
