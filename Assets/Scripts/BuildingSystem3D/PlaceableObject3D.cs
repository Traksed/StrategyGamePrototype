using System;
using UnityEngine;

namespace BuildingSystem3D
{
    public class PlaceableObject3D : MonoBehaviour
    {
        public bool Placed { get; private set; }
        public Vector3 Size { get; private set; }

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
            for (int i = 0; i < _vertices.Length; i++)
            {
                Vector3 worldPos = transform.TransformPoint(_vertices[i]);
                _vertices[i] = BuildingSystem3D.Current.gridLayout.WorldToCell(worldPos);
            }

            Size = new Vector3(Mathf.Abs((vertices[0] - vertices[1]).x),
                                Mathf.Abs((vertices[0] - vertices[3]).y), 
                                1);
        }

        private Vector3 GetStartPosition()
        {
            return transform.TransformPoint(_vertices[0]);
        }

        private void Start()
        {
            GetColliderVertexPositionLocal();
            CalculatedSizeInCells();
        }

        public virtual void Place()
        {
            ObjectDrag3D drag3D = gameObject.GetComponent<ObjectDrag3D>();
            Destroy(drag3D);

            Placed = true;
            
            //invoke events on placement
        }
    }
}
