using System.Collections;
using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    private const string LEVEL_DATA_KEY = "LevelData";
    private float INITIAL_DELAY = 0.5f;

    [SerializeField] private LevelDataSO[] _levelsData;
    [SerializeField] private LoadingScreenView _loadingScreen;
    [SerializeField] private LevelBuilder _levelBuilder;
    [SerializeField] private LoseScreenView _loseScreenView;
    [SerializeField] private ProgressView _progressView;
    [SerializeField] private FallDetector _fallDetector;
    [SerializeField] private LoseController _loseController;
    [SerializeField] private Timer _timer;
    [SerializeField] private TimerView _timerView;

    [Inject] private CubeCollector _cubeCollector;
    [Inject] private SceneLoader _sceneLoader;

    private LevelDataSO _currentLevelData;
    private WaitForSeconds _delayLoading;

    private void Start()
    {
        _delayLoading = new WaitForSeconds(INITIAL_DELAY);
        _currentLevelData = GetLevelData();

        StartCoroutine(StartingGame(_currentLevelData));
    }

    private IEnumerator StartingGame(LevelDataSO levelData)
    {
        _loadingScreen.Initialize();
        _sceneLoader.Initialize(_timer);
        _loseController.Initialize();
        _cubeCollector.Initialize(levelData, _fallDetector);
        _progressView.Initialize(levelData.TotalCubes);
        _progressView.OnUpdateCounter(_cubeCollector.CurrentCubeCount, levelData.TotalCubes);
        _timerView.Initialize(levelData);
        _timer.Initialize(levelData);
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
        StartCoroutine(_timer.StartTimer());
    }

    private LevelDataSO GetLevelData()
    {
        if (PlayerPrefs.HasKey(LEVEL_DATA_KEY))
            return _levelsData[PlayerPrefs.GetInt(LEVEL_DATA_KEY)];

        PlayerPrefs.SetInt(LEVEL_DATA_KEY, Random.Range(0, _levelsData.Length - 1));
        return _levelsData[PlayerPrefs.GetInt(LEVEL_DATA_KEY)];
    }
}
