using System;
using UnityEngine;

public class GameCurrencyManager
{
    private int _currency;

    public int Currency => _currency;

    public event Action<int> OnCurrencyChanged;
    
    public GameCurrencyManager()
    {
        _currency = 100;
    }

    public void EarnCurrency(int amount)
    {
        _currency += amount;
        OnCurrencyChanged?.Invoke(_currency);
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= _currency)
        {
            _currency -= amount;
            OnCurrencyChanged?.Invoke(_currency);
            return true;
        }
        else
        {
            Debug.Log("You don't have enough currency.");
            return false;
        }
    }
}