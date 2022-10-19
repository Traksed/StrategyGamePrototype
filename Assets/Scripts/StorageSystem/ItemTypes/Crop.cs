using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crop", menuName = "GameObject/StorageItems/Crop")]
public class Crop : Producible
{
    public Sprite GrowingCrop;
    public Sprite ReadyCrop;

    private new void OnValidate()
    {
        base.OnValidate();

        ItemsNeeded = new Dictionary<CollectibleItem, int>() { { this, 1 } };
    }
}
