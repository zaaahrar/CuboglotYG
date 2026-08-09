using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using YG;

public class CubeCollector : MonoBehaviour
{
    [Inject] private AudioController _audio;
    [SerializeField] private FallDetector _fallDetector;

    public event Action<int, int> ProgressUpdated;
    public event Action AllCubesCollected;

    private LevelDataSO _currentLevelData;
    private List<ColorCube> _collectedColors = new List<ColorCube>();
    private int _currentCubeCount = 0;

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
    }

    public void Initialize(LevelDataSO levelData, FallDetector fallDetector)
    {
        if(levelData == null || fallDetector == null)
            throw new ArgumentNullException();

        _fallDetector = fallDetector;
        _currentLevelData = levelData;

        ResetState();

        _fallDetector.CollectCube += OnCollectCube;
    }

    public void RemoveColor(ColorCube colorCube) => _collectedColors.Remove(colorCube);
    
    public void ResetState()
    {
        _currentCubeCount = 0;
        _collectedColors.Clear();
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
            AllCubesCollected?.Invoke();
            YandexGame.GameplayStop();
        }
    }

    private bool IsCompleteLevel() => CurrentCubeCount >= _currentLevelData.TotalCubes;
}
