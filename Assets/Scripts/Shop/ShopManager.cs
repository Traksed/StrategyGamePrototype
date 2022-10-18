using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Current;
    public static Dictionary<CurrencyType, Sprite> CurrencySprites = new Dictionary<CurrencyType, Sprite>();

    [SerializeField] private List<Sprite> sprites;

    private RectTransform _rt;
    private RectTransform _prt;
    private bool _opened;

    [SerializeField] private GameObject itemPrefab;
    private Dictionary<ObjectType, List<ShopItem>> _shopItems = new Dictionary<ObjectType, List<ShopItem>>(5);

    [SerializeField] public TabGroup shopTabs;

    private void Awake()
    {
        Current = this;

        _rt = GetComponent<RectTransform>();
        _prt = transform.parent.GetComponent<RectTransform>();

        EventManager.Instance.AddListener<GameEvent.LevelChangedGameEvent>(OnLevelChanged);
    }

    private void Start()
    {
        CurrencySprites.Add(CurrencyType.Coins, sprites[0]);
        CurrencySprites.Add(CurrencyType.Crystals, sprites[1]);

        Load();
        Initialize();

        gameObject.SetActive(false);
    }

    private void Load()
    {
        ShopItem[] items = Resources.LoadAll<ShopItem>("Shop");

        _shopItems.Add(ObjectType.Animals, new List<ShopItem>());
        _shopItems.Add(ObjectType.AnimalHomes, new List<ShopItem>());
        _shopItems.Add(ObjectType.ProductionBuildings, new List<ShopItem>());
        _shopItems.Add(ObjectType.TreesBushes, new List<ShopItem>());
        _shopItems.Add(ObjectType.Decorations, new List<ShopItem>());

        foreach (var item in items)
        {
            _shopItems[item.Type].Add(item);
        }
    }

    private void Initialize()
    {
        for (int i = 0; i < _shopItems.Keys.Count; i++)
        {
            foreach (var item in _shopItems[(ObjectType)i])
            {
                GameObject itemObject = Instantiate(itemPrefab, shopTabs.objectsToSwap[i].transform);
                itemObject.GetComponent<ShopItemHolder>().Initialize(item);
            }
        }
    }

    private void OnLevelChanged(GameEvent.LevelChangedGameEvent info)
    {
        for (int i = 0; i < _shopItems.Keys.Count; i++)
        {
            ObjectType key = _shopItems.Keys.ToArray()[i];
            for (int j = 0; j < _shopItems[key].Count; j++)
            {
                ShopItem item = _shopItems[key][j];

                if (item.Level == info.NewLvl)
                {
                    shopTabs.transform.GetChild(i).GetChild(j).GetComponent<ShopItemHolder>().UnlockItem();
                }
            }
        }
    }

    public void ShopButton_Click()
    {
        float time = 0.2f;
        if (!_opened)
        {
            LeanTween.moveX(_prt, _prt.anchoredPosition.x + _rt.sizeDelta.x, time);
            _opened = true;
            gameObject.SetActive(true);
        }
        else
        {
            LeanTween.moveX(_prt, _prt.anchoredPosition.x - _rt.sizeDelta.x, time)
                .setOnComplete(delegate() { gameObject.SetActive(false); });
            _opened = false;
        }
    }

    private bool _dragging;

    public void OnBeginDrag()
    {
        _dragging = true;
    }

    public void OnEndDrag()
    {
        _dragging = false;
    }

    public void OnPointerClick()
    {
        if (!_dragging)
        {
            ShopButton_Click();
        }
    }
}