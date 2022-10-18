using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    public bool Placed { get; private set; }
    private Vector3 _origin;

    public BoundsInt area;

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

    public void Place()
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
}
