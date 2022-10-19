using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    public static StorageManager current;

    [SerializeField] private GameObject barnPrefab;
    [SerializeField] private GameObject siloPrefab;

    private string _itemsPath = "Storage";
    private Dictionary<AnimalProduct, int> _animalProducts;
    private Dictionary<Crop, int> _crops;
    private Dictionary<Feed, int> _feeds;
    private Dictionary<Fruit, int> _fruits;
    private Dictionary<Product, int> _products;
    private Dictionary<Tool, int> _tools;

    private Dictionary<CollectibleItem, int> _barnItems;
    private Dictionary<CollectibleItem, int> _siloItems;


    private StorageBuilding _barn;
    private StorageBuilding _silo;

    private void Awake()
    {
        current = this;
        Dictionary<CollectibleItem, int> itemsAmounts = LoadItems();
        Sort(itemsAmounts);
    }

    private Dictionary<CollectibleItem, int> LoadItems()
    {
        Dictionary<CollectibleItem, int> itemAmounts = new Dictionary<CollectibleItem, int>();
        CollectibleItem[] allItems = Resources.LoadAll<CollectibleItem>(_itemsPath);

        for (int i = 0; i < allItems.Length; i++)
        {
            if (allItems[i].Level >= LevelSystem.Level)
            {
                //todo remove 2 in a real game
                itemAmounts.Add(allItems[i], 2);
            }
        }

        return itemAmounts;
    }

    private void Sort(Dictionary<CollectibleItem, int> itemsAmounts)
    {
        _animalProducts = new Dictionary<AnimalProduct, int>();
        _crops = new Dictionary<Crop, int>();
        _feeds = new Dictionary<Feed, int>();
        _fruits = new Dictionary<Fruit, int>();
        _products = new Dictionary<Product, int>();
        _tools = new Dictionary<Tool, int>();

        _siloItems = new Dictionary<CollectibleItem, int>();
        _barnItems = new Dictionary<CollectibleItem, int>();

        foreach (var itemPair in itemsAmounts)
        {
            if (itemPair.Key is AnimalProduct animalProduct)
            {
                _animalProducts.Add(animalProduct, itemPair.Value);
                _barnItems.Add(animalProduct, itemPair.Value);
            }
            else if (itemPair.Key is Crop crop)
            {
                _crops.Add(crop, itemPair.Value);
                _siloItems.Add(crop, itemPair.Value);
            }
            else if (itemPair.Key is Feed feed)
            {
                _feeds.Add(feed, itemPair.Value);
                _barnItems.Add(feed, itemPair.Value);
            }
            else if (itemPair.Key is Fruit fruit)
            {
                _fruits.Add(fruit, itemPair.Value);
                _siloItems.Add(fruit, itemPair.Value);
            }
            else if (itemPair.Key is Product product)
            {
                _products.Add(product, itemPair.Value);
                _barnItems.Add(product, itemPair.Value);
            }
            else if (itemPair.Key is Tool tool)
            {
                _tools.Add(tool, itemPair.Value);
                _barnItems.Add(tool, itemPair.Value);
            }
        }
    }

    private void Start()
    {
        GameObject siloObject = BuildingSystem.current.InitializeWithObject(siloPrefab, new Vector3(7.25f, -0.25f));
        _silo = siloObject.GetComponent<StorageBuilding>();
        _silo.Load();
        _silo.Initialize(_siloItems,"Silo");

        GameObject barnObject = BuildingSystem.current.InitializeWithObject(barnPrefab, new Vector3(6f, -0.25f));
        _barn = barnObject.GetComponent<StorageBuilding>();
        _barn.Load();
        _barn.Initialize(_barnItems,"Barn");
    }

    public int GetAmount(CollectibleItem item)
    {
        int amount = 0;
        if (item is AnimalProduct animalProduct)
        {
            _animalProducts.TryGetValue(animalProduct, out amount);
        }
        else if (item is Crop crop)
        {
            _crops.TryGetValue(crop, out amount);
        }
        else if (item is Feed feed)
        {
            _feeds.TryGetValue(feed, out amount);
        }
        else if (item is Fruit fruit)
        {
            _fruits.TryGetValue(fruit, out amount);
        }
        else if (item is Product product)
        {
            _products.TryGetValue(product, out amount);
        }
        else if (item is Tool tool)
        {
            _tools.TryGetValue(tool, out amount);
        }

        return amount;
    }

    public bool IsEnoughOf(CollectibleItem item, int amount)
    {
        return GetAmount(item) >= amount;
    }
}
