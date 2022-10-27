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

            if (!_objectToPlace)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                //work
                _objectToPlace.Rotate();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (CanBePlaced(_objectToPlace))
                {
                    _objectToPlace.Place();
                    Vector3Int start = gridLayout.WorldToCell(_objectToPlace.GetStartPosition());
                    TakeArea(start, _objectToPlace.Size);
                }
                else
                {
                    Destroy(_objectToPlace.gameObject);
                }
            }
        }

        #endregion

        #region Utils

       
        public static Vector3 GetMouseWorldPosition()
        {
            //work
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
            //work
            Vector3Int cellPosition = gridLayout.WorldToCell(position);
            position = _grid.GetCellCenterWorld(cellPosition);
            return position;
        }

        private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
        {
            TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
            int counter = 0;
            foreach (var vector in area.allPositionsWithin)
            {
                Vector3Int position = new Vector3Int(vector.x, vector.y, vector.z);
                array[counter] = tilemap.GetTile(position);
                counter++;
            }

            return array;
        }

       

        #endregion

        #region BuildingPlacement

        public void InitializeWithObject(GameObject prefab)
        {
            //work
            Vector3 position = SnapCoordinateToGrid(Vector3.zero);

            GameObject obj = Instantiate(prefab, position, Quaternion.identity);
            _objectToPlace = obj.GetComponent<PlaceableObject3D>();
            obj.AddComponent<ObjectDrag3D>();
        }

        private bool CanBePlaced(PlaceableObject3D placeableObject3D)
        {
            BoundsInt area = new BoundsInt();
            area.position = gridLayout.WorldToCell(_objectToPlace.GetStartPosition());
            area.size = new Vector3Int(area.size.x + 1, area.size.y + 1, area.size.z);
            //area.size = placeableObject3D.Size;

            TileBase[] baseArray = GetTilesBlock(area, mainTilemap);

            foreach (var tileBase in baseArray)
            {
                if (tileBase == baseTile)
                {
                    return false;
                }
            }

            return true;
        }

        public void TakeArea(Vector3Int start, Vector3Int size)
        {
            mainTilemap.BoxFill(start, baseTile, start.x, start.y, 
                            start.x + size.x, start.y + size.y);
        }

        #endregion
    }
}
