using UnityEngine;
using UnityEngine.UI;
using Zenject;
using YG;

public class PauseView : MonoBehaviour
{
    [Inject] private AudioController _audio;

    [SerializeField] private GameObject _window;
    [SerializeField] private PauseController _controller;
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;

    [SerializeField] private Button _exitInMenuButton;
    [SerializeField] private Button _restartButton;

    private void Start()
    {
        _openButton.onClick.AddListener(OnShow);
        _closeButton.onClick.AddListener(OnHide);
        _exitInMenuButton.onClick.AddListener(_controller.ExitInMenu);
        _restartButton.onClick.AddListener(_controller.Restart);
        //YandexGame.onHideWindowGame += OnShow;
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(OnShow);
        _closeButton.onClick.RemoveListener(OnHide);
        _exitInMenuButton.onClick.RemoveListener(OnExitInMenu);
        _restartButton.onClick.RemoveListener(OnRestart);
        //YandexGame.onHideWindowGame -= OnShow;
    }

    private void OnExitInMenu()
    {
        _audio.PlayClickSound();
        _controller.ExitInMenu();
    }

    private void OnRestart()
    {
        _audio.PlayClickSound();
        _controller.Restart();
    }

    private void OnShow()
    {
        _audio.PlayClickSound();
        _window.SetActive(true);
        YandexGame.GameplayStop();
        Time.timeScale = 0;
    }

    private void OnHide()
    {
        Time.timeScale = 1;
        YandexGame.GameplayStart();
        _audio.PlayClickSound();
        _window.SetActive(false);
    }

}
