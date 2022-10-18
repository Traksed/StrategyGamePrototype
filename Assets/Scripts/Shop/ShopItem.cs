using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "GameObjects/ShopItem", order = 0)]
public class ShopItem : MonoBehaviour
{
    public string Name = "Default";
    public string Description = "Description";
    public int Level;
    public int Price;
    public CurrencyType Currency;
    public ObjectType Type;
    public Sprite Icon;
    public GameObject Prefab;
}

public enum ObjectType
{
    AnimalHomes,
    Animals,
    ProductionBuildings,
    TreesBushes,
    Decorations
}