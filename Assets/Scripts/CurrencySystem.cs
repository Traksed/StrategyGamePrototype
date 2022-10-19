using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencySystem : MonoBehaviour
{
    private static Dictionary<CurrencyType, int> CurrencyAmounts = new Dictionary<CurrencyType, int>();

    [SerializeField] private List<GameObject> texts;

    private Dictionary<CurrencyType, TextMeshProUGUI> currencyTexts = new Dictionary<CurrencyType, TextMeshProUGUI>();

    private void Awake()
    {
        for (int i = 0; i < texts.Count; i++)
        {
            CurrencyAmounts.Add((CurrencyType)i, 0);
            currencyTexts.Add((CurrencyType)i, texts[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>());
        }
    }

    private void Start()
    {
        CurrencyAmounts[CurrencyType.Coins] = 100;
        CurrencyAmounts[CurrencyType.Crystals] = 10;
        UpdateUI();
        
        EventManager.Instance.AddListener<GameEvent.CurrencyChangeGameEvent>(OnCurrencyChange);
        EventManager.Instance.AddListener<GameEvent.NotEnoughCurrencyGameEvent>(OnNotEnough);
    }

    private void UpdateUI()
    {
        for (int i = 0; i < texts.Count; i++)
        {
            currencyTexts[(CurrencyType)i].text = CurrencyAmounts[(CurrencyType)i].ToString();
        }
    }

    private void OnCurrencyChange(GameEvent.CurrencyChangeGameEvent info)
    {
        if (info.Amount < 0)
        {
            if (CurrencyAmounts[info.currencyType] < Math.Abs(info.Amount))
            {
                EventManager.Instance.QueueEvent(
                    new GameEvent.NotEnoughCurrencyGameEvent(info.Amount, info.currencyType));
                return;
            }

            EventManager.Instance.QueueEvent(new GameEvent.EnoughCurrencyGameEvent());
        }
        
        CurrencyAmounts[info.currencyType] += info.Amount;
        currencyTexts[info.currencyType].text = CurrencyAmounts[info.currencyType].ToString();
        
        UpdateUI();
    }

    private void OnNotEnough(GameEvent.NotEnoughCurrencyGameEvent info)
    {
        Debug.Log($"You don't have enough of {info.Amount} {info.currencyType}");
    }
}

public enum CurrencyType
{
    Coins,
    Crystals
}
