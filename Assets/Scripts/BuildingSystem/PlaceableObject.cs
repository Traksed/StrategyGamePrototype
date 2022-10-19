using Unity.Collections;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    private ShopItem _item;
    public bool Placed { get; private set; }
    private Vector3 _origin;

    public BoundsInt area;

    [ReadOnly()] public PlaceableObjectData data = new PlaceableObjectData();

    public void Initialize(ShopItem shopItem)
    {
        _item = shopItem;
        data.AssetName = _item.Name;
        data.ID = SaveData.GenerateId();
    }

    public void Initialize(ShopItem shopItem, PlaceableObjectData objectData)
    {
        _item = shopItem;
        data = objectData;
    }

    public void Load()
    {
        Destroy(GetComponent<ObjectDrag>());
        Place();
    }

    public bool CanBePlaced()
    {
        Vector3Int positionInt = BuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if (BuildingSystem.current.CanTakeArea(areaTemp))
        {
            return true;
        }

        return false;
    }

    public virtual void Place()
    {
        Vector3Int positionInt = BuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        Placed = true;
        
        BuildingSystem.current.TakeArea(areaTemp);
    }

    public void CheckPlacement()
    {
        if (!Placed)
        {
            if (CanBePlaced())
            {
                Place();
                _origin = transform.position;
            }
            else
            {
                Destroy(transform.gameObject);
            }
        
            ShopManager.Current.ShopButton_Click();
        }
        else
        {
            if (CanBePlaced())
            {
                Place();
                _origin = transform.position;
            }
            else
            {
                transform.position = _origin;
                Place();
            }
        }
    }

    private float _time = 0f;
    private bool _touching;

    private void Update()
    {
        if (!_touching && Placed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _time = 0;
            }
            else if (Input.GetMouseButton(0))
            {
                _time += Time.deltaTime;

                if (_time > 3f)
                {
                    _touching = true;
                    gameObject.AddComponent<ObjectDrag>();

                    Vector3Int positionInt = BuildingSystem.current.gridLayout.WorldToCell(transform.position);
                    BoundsInt areaTemp = area;
                    areaTemp.position = positionInt;

                    BuildingSystem.current.ClearArea(areaTemp, BuildingSystem.current.mainTilemap);
                }
            }
        }

        if (_touching && Input.GetMouseButtonUp(0))
        {
            _touching = false;
        }
    }

    private void OnApplicationQuit()
    {
        data.position = transform.position;
        GameManager.Current.SaveData.AddData(data);
    }
}
