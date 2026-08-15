using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using YG;

public class CubeCollector : MonoBehaviour
{
    [Inject] private AudioController _audio;
    [Inject] private GameSettingsSO _gameSettings;
    [SerializeField] private FallDetector _fallDetector;

    public event Action<int, int> ProgressUpdated;
    public event Action LevelComplete;
    public event Action<string, LoseReason> LoseGame;

    private LevelDataSO _currentLevelData;
    private Timer _timer;
    private List<ColorCube> _collectedColors = new List<ColorCube>();
    private int _currentCubeCount = 0;

    private string _loseDescriptionRU; 
    private string _loseDescriptionEN;
    private string _loseDescriptionTR;

    public IReadOnlyList<ColorCube> CollectCubeColors => _collectedColors;
    public int CurrentCubeCount
    {
        get => _currentCubeCount;
        private set
        {
            _currentCubeCount = value;
            ProgressUpdated?.Invoke(_currentCubeCount, _currentLevelData.TotalCubes);
        }
    }

    private void OnDisable()
    {
        if (_fallDetector == null)
            return;

        _fallDetector.CollectCube -= OnCollectCube;
        _timer.TimerFinished -= OnCheckWin;
    }

    public void Initialize(LevelDataSO levelData, FallDetector fallDetector, Timer timer)
    {
        if(levelData == null || fallDetector == null || timer == null)
            throw new ArgumentNullException();

        int winPercent = Mathf.RoundToInt(_gameSettings.VictoryPercentage * 100);

        _loseDescriptionRU = $"Вы не успели собрать достаточно кубов. Чтобы пройти уровень, нужно собрать минимум {winPercent}% от всех кубов";
        _loseDescriptionEN = $"You didn't collect enough cubes in time. To pass the level, you need to collect at least {winPercent}% of all cubes";
        _loseDescriptionTR = $"Yeterince küp toplayamadınız. Seviyeyi geçmek için tüm küplerin en az {winPercent}% sini toplamanız gerekiyor";

        _fallDetector = fallDetector;
        _currentLevelData = levelData;
        _timer = timer;

        ResetState();

        _fallDetector.CollectCube += OnCollectCube;
        _timer.TimerFinished += OnCheckWin;
    }

    public void RemoveColor(ColorCube colorCube) => _collectedColors.Remove(colorCube);
    
    public void ResetState()
    {
        _currentCubeCount = 0;
        _collectedColors.Clear();
    }

    private void OnCheckWin()
    {
        if (_currentCubeCount / _currentLevelData.TotalCubes < _gameSettings.VictoryPercentage || _currentCubeCount == 0)
            LoseGame?.Invoke(Utils.GetTranslateText(_loseDescriptionRU, _loseDescriptionTR, _loseDescriptionEN), LoseReason.NotEnoughCubes);
        else
            LevelComplete?.Invoke();
    }

    private void OnCollectCube(Cube cube)
    {
        if(cube == null)
            throw new ArgumentNullException(nameof(cube));

        CurrentCubeCount++;
        _audio.PlayCollectSound();
        _collectedColors.Add(cube.CurrentColor);
        Destroy(cube.gameObject);

        if (IsCompleteLevel())
        {
            LevelComplete?.Invoke();
            YandexGame.GameplayStop();
        }
    }

    private bool IsCompleteLevel() => CurrentCubeCount >= _currentLevelData.TotalCubes;
}
