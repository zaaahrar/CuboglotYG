using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class LoseScreenView : MonoBehaviour
{
    private const int GameplayScene = 1;

    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitMenuButton;
    [SerializeField] private FallObjectsTriggerCheker _triggerChecker;

    public void Initialize()
    {
        _restartButton.onClick.AddListener(OnRestartingGame);
        _exitMenuButton.onClick.AddListener(OnExitInMenu);
        _triggerChecker.CollectBomb += OnGameLose;
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartingGame);
        _exitMenuButton.onClick.RemoveListener(OnExitInMenu);
        _triggerChecker.CollectBomb -= OnGameLose;
    }

    public void OnGameLose()
    {
        Time.timeScale = 0;
        _loseScreen.SetActive(true);
    }

    private void OnExitInMenu()
    {
        Debug.Log("Выход в меню.");
    }

    private void OnRestartingGame()
    {
        Hide();
        SceneManager.LoadScene(GameplayScene);
    }
    private void Hide()
    {
        Time.timeScale = 1;
        _loseScreen.SetActive(false);
    }
}
