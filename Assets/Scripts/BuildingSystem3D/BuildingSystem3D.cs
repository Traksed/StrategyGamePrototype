using UnityEngine;
using UnityEngine.Tilemaps;

namespace BuildingSystem3D
{
    public class  BuildingSystem3D: MonoBehaviour
    {
        public static BuildingSystem3D Current;
        
        public GridLayout gridLayout;
        public GameObject prefab1;
        public GameObject prefab2;

        private Grid _grid;
        private PlaceableObject3D _objectToPlace;

        [SerializeField] private Tilemap mainTilemap;
        [SerializeField] private TileBase baseTile;

        #region Unity methods

        private void Awake()
        {
            Current = this;
            _grid = gridLayout.gameObject.GetComponent<Grid>();
        }

        #endregion

        #region Unils

        public static Vector3 GetMouseWorldPosition()
        {
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                return raycastHit.point;
            }
            else
            {
                return Vector3.zero;
            }
        }

        public Vector3 SnapCoordinateToGrid(Vector3 position)
        {
            Vector3Int cellPosition = gridLayout.WorldToCell(position);
            position = _grid.GetCellCenterWorld(cellPosition);
            return position;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                InitializeWithObject(prefab1);
            }
            else if(Input.GetKeyDown(KeyCode.B))
            {
                InitializeWithObject(prefab2);
            }
        }

        #endregion

        #region BuildingPlacement

        public void InitializeWithObject(GameObject prefab)
        {
            Vector3 position = SnapCoordinateToGrid(Vector3.zero);

            GameObject obj = Instantiate(prefab, position, Quaternion.identity);
            _objectToPlace = obj.GetComponent<PlaceableObject3D>();
            obj.AddComponent<ObjectDrag3D>();
        }

        #endregion
    }
}
