using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Current;

    public SaveData SaveData;
    [SerializeField] private string shopItemsPath = "Shop";
    
    public GameObject canvas;

    private void Awake()
    {
        Current = this;

        ShopItemDrag.Canvas = canvas.GetComponent<Canvas>();
        
        SaveSystem.Initialize();
    }

    private void Start()
    {
        SaveData = SaveSystem.Load();
        LoadGame();
    }

    private void LoadGame()
    {
        LoadPlaceableObjects();
    }

    private void LoadPlaceableObjects()
    {
        foreach (var plObjData in SaveData.placeableObjectDatas.Values)
        {
            try
            {
                ShopItem item = Resources.Load<ShopItem>(shopItemsPath + "/" + plObjData.AssetName);
                GameObject obj = BuildingSystem.current.InitializeWithObject(item.Prefab, plObjData.position);
                PlaceableObject plObj = obj.GetComponent<PlaceableObject>();
                plObj.Initialize(item, plObjData);
                plObj.Load();
            }
            catch (Exception e)
            {
                // Console.WriteLine(e);
                // throw;
            }
        }
    }

    public void GetXP(int amount)
    {
        GameEvent.XPAddedGameEvent info = new GameEvent.XPAddedGameEvent(amount);
        EventManager.Instance.QueueEvent(info);
    }

    public void GetCoins(int amount)
    {
        GameEvent.CurrencyChangeGameEvent info = new GameEvent.CurrencyChangeGameEvent(amount, CurrencyType.Coins);
        
        EventManager.Instance.QueueEvent(info);
    }

    private void OnDisable()
    {
        SaveSystem.Save(SaveData);
    }
}
