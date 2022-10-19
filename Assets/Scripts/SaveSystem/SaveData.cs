using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class SaveData : MonoBehaviour
{
    public static int IdCount;

    public Dictionary<string, PlaceableObjectData> placeableObjectDatas = new Dictionary<string, PlaceableObjectData>();

    public static string GenerateId()
    {
        IdCount++;
        return IdCount.ToString();
    }

    public void AddData(Data data)
    {
        if (data is PlaceableObjectData plObjData)
        {
            if (placeableObjectDatas.ContainsKey(plObjData.ID))
            {
                placeableObjectDatas[plObjData.ID] = plObjData;
            }
            else
            {
                placeableObjectDatas.Add(plObjData.ID, plObjData);
            }
        }
    }

    public void RemoveData(Data data)
    {
        if (data is PlaceableObjectData plObjData)
        {
            if (placeableObjectDatas.ContainsKey(plObjData.ID))
            {
                placeableObjectDatas.Remove(plObjData.ID);
            }
        }
    }

    [OnDeserialized]
    internal void OnDeserializedMethod(StreamingContext context)
    {
        placeableObjectDatas ??= new Dictionary<string, PlaceableObjectData>();
    }
}
