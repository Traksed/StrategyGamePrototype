using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemHolder : MonoBehaviour
{
    private ShopItem _item;
    private LevelSystem _levelSystem;

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private Image iconImage;
    [SerializeField] private Image currencyImage;
    [SerializeField] private TextMeshProUGUI priceText;

    public void Initialize(ShopItem item)
    {
        _item = item;
    
        iconImage.sprite = _item.Icon;
        titleText.text = _item.Name;
        descriptionText.text = _item.Description;
        currencyImage.sprite = ShopManager.CurrencySprites[_item.Currency];
        priceText.text = _item.Price.ToString();

        if (_item.Level >= LevelSystem.Level)
        {
            UnlockItem();
        }
    }

    public void UnlockItem()
    {
        iconImage.gameObject.AddComponent<ShopItemDrag>().Initialize(_item);
        iconImage.transform.GetChild(0).gameObject.SetActive(true);
    }
}
