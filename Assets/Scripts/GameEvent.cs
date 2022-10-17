using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public class CurrencyChangeGameEvent : GameEvent
    {
        public int Amount;
        public CurrencyType currencyType;

        public CurrencyChangeGameEvent(int amount, CurrencyType currencyType)
        {
            this.Amount = amount;
            this.currencyType = currencyType;
        }
    }
    
    public class EnoughCurrencyGameEvent : GameEvent
    {
        
    }

    public class NotEnoughCurrencyGameEvent : GameEvent
    {
        public int Amount;
        public CurrencyType currencyType;

        public NotEnoughCurrencyGameEvent(int amount, CurrencyType currencyType)
        {
            this.Amount = amount;
            this.currencyType = currencyType;
        }
    }
    
    public class XPAddedGameEvent : GameEvent
    {
        public int Amount;

        public XPAddedGameEvent(int amount)
        {
            this.Amount = amount;
        }
    }
    
    public class LevelChangedGameEvent : GameEvent
    {
        public int NewLvl;

        public LevelChangedGameEvent(int currLvl)
        {
            NewLvl = currLvl;
        }
    }
}
