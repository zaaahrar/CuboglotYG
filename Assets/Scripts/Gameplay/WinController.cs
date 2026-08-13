using System;
using UnityEngine;
using YG;
using Zenject;

public class WinController : MonoBehaviour
{
    private const string LeaderboardName = "Leaderboard";
    private const int OneStarPercent = 30;
    private const int TwoStarsPercent = 60;
    private const int ThreStarsPercent = 100;
    private const int MaxStars = 3;

    [Inject] private GameSettingsSO _settings;
    [Inject] private GoldHandler _goldHandler;

    public event Action ShowWinScreen;
    public event Action<int, int, int, int> UpdateWinScreen;
    public event Action SuccessfulAdvertising;

    private int _starsCount = 0;
    private LevelDataSO _levelData;
    private int _cubesCollect;

    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;

    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    public void Win(int cubesCollect)
    {
        _cubesCollect = cubesCollect;
        _levelData = _settings.GetCurrentLevel();
        _starsCount = GetStars(_cubesCollect);
        int gold = GetGold(_cubesCollect);
        _goldHandler.AddGold(gold);
        TryAddRecordLiderboard();
        UpdateWinScreen?.Invoke(_cubesCollect, _levelData.TotalCubes, gold, _starsCount);
        ShowWinScreen?.Invoke();
    }

    public void ShowAD() => YandexGame.RewVideoShow(AdPlacementIds.X2GoldReward);

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

    private void TryAddRecordLiderboard()
    {
        if(_starsCount == MaxStars)
        {
            YandexGame.savesData.LeaderboardScore++;
            YandexGame.NewLeaderboardScores(LeaderboardName, YandexGame.savesData.LeaderboardScore);
            YandexGame.SaveProgress();
        }
    }

    private void Rewarded(int index)
    {
        if (index == AdPlacementIds.X2GoldReward)
        {
            int gold = GetGold(_cubesCollect);
            _goldHandler.AddGold(gold);
            UpdateWinScreen?.Invoke(_cubesCollect, _levelData.TotalCubes, gold * 2, _starsCount);
            SuccessfulAdvertising?.Invoke();
        }

    }
}
