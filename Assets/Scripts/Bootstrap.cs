using System.Collections;
using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    private const string LevelDataKey = "LevelData";

    [SerializeField] private LevelDataSO[] _levelsData;
    [SerializeField] private LoadingScreenView _loadingScreen;
    [SerializeField] private LevelBuilder _levelBuilder;
    [SerializeField] private LoseScreenView _loseScreenView;
    [SerializeField] private ProgressView _progressView;
    
    private CubeCounter _cubeCounter;

    private LevelDataSO _currentLevelData;
    private float _delay = 0.5f;
    private WaitForSeconds _delayLoading;

    [Inject]
    public void Construct(CubeCounter cubeCounter)
    {
        _cubeCounter = cubeCounter;
    }

    void Start()
    {
        _delayLoading = new WaitForSeconds(_delay);

        if (PlayerPrefs.HasKey(LevelDataKey))
        {
            _currentLevelData = _levelsData[PlayerPrefs.GetInt(LevelDataKey)];
        }
        else
        {
            PlayerPrefs.SetInt(LevelDataKey, GetRandomLevel());
            _currentLevelData = _levelsData[PlayerPrefs.GetInt(LevelDataKey)];
        }

        if (_cubeCounter == null)
            Debug.Log("null");

        StartCoroutine(StartingGame(_currentLevelData));
    }

    private IEnumerator StartingGame(LevelDataSO levelData)
    {
        _loadingScreen.Initialize();
        _cubeCounter.Initialize(levelData);
        _progressView.Initialize(levelData.TotalCubes);
        _progressView.OnUpdateCounter(_cubeCounter.CountCubes, levelData.TotalCubes);
        _loseScreenView.Initialize();
        _loadingScreen.Show();
        yield return _delayLoading;
        _loadingScreen.ChangeSlider(34);
        yield return StartCoroutine(_levelBuilder.BuildingLevel(levelData));
        _loadingScreen.ChangeSlider(78);
        yield return _delayLoading;
        _loadingScreen.ChangeSlider(100);
        yield return _delayLoading;
        _loadingScreen.Hide();
    }

    private int GetRandomLevel()
    {
        return Random.Range(0, _levelsData.Length - 1);
    }
}
