using UnityEngine;

namespace BuildingSystem3D
{
    public class PlaceableObject3D : MonoBehaviour
    {
        public Vector3Int Size { get; private set; }

        private Vector3[] _vertices;

        private void GetColliderVertexPositionLocal()
        {
            BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
            _vertices = new Vector3[4];
            var size = boxCollider.size;
            var center = boxCollider.center;
            _vertices[0] = center + new Vector3(-size.x, -size.y, -size.z) * 0.5f;
            _vertices[1] = center + new Vector3(size.x, -size.y, -size.z) * 0.5f;
            _vertices[2] = center + new Vector3(size.x, -size.y, size.z) * 0.5f;
            _vertices[3] = center + new Vector3(-size.x, -size.y, size.z) * 0.5f;
        }

        private void CalculatedSizeInCells()
        {
            Vector3Int[] vertices = new Vector3Int[_vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 worldPos = transform.TransformPoint(_vertices[i]);
                vertices[i] = BuildingSystem3D.Current.gridLayout.WorldToCell(worldPos);
            }

            Size = new Vector3Int(Mathf.Abs((vertices[0] - vertices[1]).x),
                                Mathf.Abs((vertices[0] - vertices[3]).y), 
                                1);
        }

        public Vector3 GetStartPosition()
        {
            return transform.TransformPoint(_vertices[0]);
        }

        private void Start()
        {
            GetColliderVertexPositionLocal();
            CalculatedSizeInCells();
        }

        public void Rotate()
        {
            transform.Rotate(new Vector3(0,90,0));
            Size = new Vector3Int(Size.y, Size.x, 1);

            Vector3[] vertices = new Vector3[_vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = _vertices[(i + 1) % _vertices.Length];
            }

            _vertices = vertices;
        }

        public virtual void Place()
        {
            ObjectDrag3D drag3D = gameObject.GetComponent<ObjectDrag3D>();
            Destroy(drag3D);

            //invoke events on placement
        }
    }
}
