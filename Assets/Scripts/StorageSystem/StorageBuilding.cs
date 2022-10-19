using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StorageBuilding : PlaceableObject
{
    private StorageUI _storageUI;

    private int _currentTotal = 0;
    private int _storageMax = 100;

    public string Name { get; private set; }

    [SerializeField] private GameObject windowPrefab;
    [SerializeField] private List<Tool> itemsToIncrease;

    private Dictionary<CollectibleItem, int> _items;
    private Dictionary<CollectibleItem, int> _tools;

    public void Initialize(Dictionary<CollectibleItem, int> itemAmounts, string name)
    {
        Name = name;
        _storageUI.SetNameText(name);

        GameObject window = Instantiate(windowPrefab, GameManager.Current.canvas.transform);
        window.SetActive(false);
        _storageUI = window.GetComponent<StorageUI>();
        
        _storageUI.SetNameText(name);

        _items = itemAmounts;
        _currentTotal = itemAmounts.Values.Sum();

        _tools = new Dictionary<CollectibleItem, int>();
        foreach (var item in itemsToIncrease)
        {
            _tools.Add(item, 1);
        }
        
        _storageUI.Initialize(_currentTotal, _storageMax, _items, _tools, IncreaseStorage);
    }

    private void IncreaseStorage()
    {
        foreach (var toolPair in _tools)
        {
            if (!StorageManager.current.IsEnoughOf(toolPair.Key, toolPair.Value))
            {
                Debug.Log("Not enough tools");
            }
        }
        
        //todo take items from storage

        _storageMax += 50;
        
        _storageUI.Initialize(_currentTotal, _storageMax, _items, _tools, IncreaseStorage);
    }

    public virtual void OnClick()
    {
        _storageUI.gameObject.SetActive(true);
    }

    private void OnMouseUpAsButton()
    {
        OnClick();
    }
}
