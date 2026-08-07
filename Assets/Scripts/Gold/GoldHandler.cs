using System;
using UnityEngine;

public class GoldHandler : MonoBehaviour
{
    [SerializeField] private int _currentGold;

    public event Action<int> UpdateGold;

    public int CurrentGold
    {
        get => _currentGold;
        private set
        {
            _currentGold = value;
            UpdateGold?.Invoke(_currentGold);
        }
    }

    private void Start()
    {
        CurrentGold = 100000;
    }

    public void AddGold(int gold)
    {
        if(gold < 0)
            throw new ArgumentOutOfRangeException(nameof(gold));

        CurrentGold += gold;
    }

    public void SpendGold(int gold)
    {
        if (gold < 0)
            throw new ArgumentOutOfRangeException(nameof(gold));

        CurrentGold -= gold;
    }

    public bool TrySpendGold(int gold)
    {
        if (gold < 0)
            throw new ArgumentOutOfRangeException(nameof(gold));

        if (_currentGold >= gold)
            return true;

        return false;
    }
}
