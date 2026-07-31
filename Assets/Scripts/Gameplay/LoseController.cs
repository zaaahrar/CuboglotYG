using UnityEngine;
using Zenject;
using System;

public class LoseController : MonoBehaviour
{
    [SerializeField] private FallDetector _fallDetector;
    [Inject] private SceneLoader _sceneLoader;

    public event Action HideWindow;
    public event Action ShowWindow;

    private void OnDisable() => _fallDetector.GameLose -= OnGameLose;

    public void Initialize() => _fallDetector.GameLose += OnGameLose;

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
}
