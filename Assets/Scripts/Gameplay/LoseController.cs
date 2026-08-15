using UnityEngine;
using Zenject;
using System;
using YG;

public class LoseController : MonoBehaviour
{
    [SerializeField, TextArea] private string _descriptionErrorRU = "Неудачное воспроизведение рекламы";
    [SerializeField, TextArea] private string _descriptionErrorEN = "Unsuccessful advertisement playback";
    [SerializeField, TextArea] private string _descriptionErrorTR = "Reklamların oynatılamaması";

    [SerializeField] private FallDetector _fallDetector;
    [SerializeField] private ErrorWindowController _errorController;
    [Inject] private CubeCollector _cubeCollector;
    [Inject] private SceneLoader _sceneLoader;

    public event Action HideWindow;
    public event Action<string, LoseReason> ShowWindow;
    public event Action ContinueGame;


    private void OnDisable()
    {
        _fallDetector.GameLose -= OnGameLose;
        YandexGame.RewardVideoEvent -= Rewarded;
        YandexGame.ErrorVideoEvent -= OnErrorVideo;
        _cubeCollector.LoseGame -= OnGameLose;
    }

    public void Initialize()
    {
        _fallDetector.GameLose += OnGameLose;
        YandexGame.RewardVideoEvent += Rewarded;
        YandexGame.ErrorVideoEvent += OnErrorVideo;
        _cubeCollector.LoseGame += OnGameLose;
    }

    public void OnGameLose(string loseDescription, LoseReason loseReason)
    {
        ShowWindow?.Invoke(loseDescription, loseReason);
        Time.timeScale = 0;
    }

    public void ExitInMenu()
    {
        Time.timeScale = 1;
        HideWindow?.Invoke();
        _sceneLoader.LoadMainMenuScene();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        HideWindow?.Invoke();
        _sceneLoader.LoadGameplayScene();
    }

    public void ShowAD() => YandexGame.RewVideoShow(AdPlacementIds.ContinueGameReward);

    private void Rewarded(int index)
    {
        if (index == AdPlacementIds.ContinueGameReward)
            ContinueGame?.Invoke();
    }

    private void OnErrorVideo() => _errorController.ShowError(Utils.GetTranslateText(_descriptionErrorRU,
        _descriptionErrorTR, _descriptionErrorEN));
}
