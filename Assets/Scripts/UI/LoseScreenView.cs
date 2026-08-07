using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoseScreenView : MonoBehaviour
{
    [Inject] private AudioController _audio;
    [SerializeField] private LoseController _controller;
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitMenuButton;

    public void Initialize()
    {
        _restartButton.onClick.AddListener(OnRestarting);
        _exitMenuButton.onClick.AddListener(OnExitInMenu);

        _controller.ShowWindow += OnShow;
        _controller.HideWindow += OnHide;
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestarting);
        _exitMenuButton.onClick.RemoveListener(OnExitInMenu);


        _controller.ShowWindow -= OnShow;
        _controller.HideWindow -= OnHide;
    }

    private void OnExitInMenu()
    {
        _audio.PlayClickSound();
        _controller.ExitInMenu();
    }

    private void OnRestarting()
    {
        _audio.PlayClickSound();
        _controller.RestartGame();
    }

    private void OnHide()
    {
        _loseScreen.SetActive(false);
        _audio.PlayClickSound();
    }

    private void OnShow()
    {
        _loseScreen.SetActive(true);
        _audio.PlayBoomSound();
    }
}
