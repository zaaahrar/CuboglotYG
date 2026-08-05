using System;
using UnityEngine;
using Zenject;

public class WinController : MonoBehaviour
{
    private const int OneStarPercent = 30;
    private const int TwoStarsPercent = 60;
    private const int ThreStarsPercent = 100;

    [Inject] private GameSettingsSO _settings;
    [Inject] private GoldHandler _goldHandler;

    public event Action ShowWinScreen;
    public event Action<int, int, int, int> UpdateWinScreen;

    private int _starsCount = 0;
    private LevelDataSO _levelData;

    public void Win(int cubesCollect)
    {
        _levelData = _settings.GetLevel();
        _starsCount = GetStars(cubesCollect);
        int gold = GetGold(cubesCollect);
        _goldHandler.AddGold(gold);
        UpdateWinScreen?.Invoke(cubesCollect, _levelData.TotalCubes, gold, _starsCount);
        ShowWinScreen?.Invoke();
    }

    private int GetStars(int cubesCollect)
    {
        float percent = cubesCollect * 100 / _levelData.TotalCubes;

        if (percent >= ThreStarsPercent) return 3;
        if (percent >= TwoStarsPercent) return 2;
        if (percent >= OneStarPercent) return 1;

        return 0;
    }

    private int GetGold(int cubes)
    {
        switch (_starsCount)
        {
            case 1:
                return cubes * (100 + 20) / 100;
            case 2:
                return cubes * (100 + 40) / 100;
            case 3:
                return cubes * (100 + 60) / 100;
            default:
                return cubes;
        }
    }
}
