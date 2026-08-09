using UnityEngine;
using Zenject;
using System;
using YG;

public class LoseController : MonoBehaviour
{
    [SerializeField, TextArea] private string _descriptionError = "Неудачное воспроизведение рекламы";
    [SerializeField] private FallDetector _fallDetector;
    [SerializeField] private ErrorWindowController _errorController;
    [Inject] private SceneLoader _sceneLoader;

    public event Action HideWindow;
    public event Action ShowWindow;
    public event Action ContinueGame;

    private void OnDisable()
    {
        _fallDetector.GameLose -= OnGameLose;
        YandexGame.RewardVideoEvent -= Rewarded;
        YandexGame.ErrorVideoEvent -= OnErrorVideo;
    }

    public void Initialize()
    {
        _fallDetector.GameLose += OnGameLose;
        YandexGame.RewardVideoEvent += Rewarded;
        YandexGame.ErrorVideoEvent += OnErrorVideo;
    }

    public void OnGameLose()
    {
        ShowWindow?.Invoke();
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

    private void OnErrorVideo() => _errorController.ShowError(_descriptionError);
}
