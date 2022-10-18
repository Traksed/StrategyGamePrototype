using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Current;
    public GameManager canvas;

    private void Awake()
    {
        Current = this;

        ShopItemDrag.Canvas = canvas.GetComponent<Canvas>();
    }

    /*public void GetXP(int amount)
    {
        GameEvent.XPAddedGameEvent info = new GameEvent.XPAddedGameEvent(amount);
        EventManager.Instance.QueueEvent(info);
    }

    public void GetCoins(int amount)
    {
        GameEvent.CurrencyChangeGameEvent info = new GameEvent.CurrencyChangeGameEvent(amount, CurrencyType.Coins);
        
        EventManager.Instance.QueueEvent(info);
    }*/
}
