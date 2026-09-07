using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoseScreenView : MonoBehaviour
{
    [Inject] private AudioController _audio;
    [SerializeField] private LoseController _controller;
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private TMP_Text _loseDecription;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitMenuButton;
    [SerializeField] private Button _advertisingButton;

    public void Initialize()
    {
        _restartButton.onClick.AddListener(OnRestarting);
        _exitMenuButton.onClick.AddListener(OnExitInMenu);
        _advertisingButton.onClick.AddListener(_controller.ShowAD);

        _controller.ShowWindow += OnShow;
        _controller.HideWindow += OnHide;
        _controller.ContinueGame += OnConinueGame;
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestarting);
        _exitMenuButton.onClick.RemoveListener(OnExitInMenu);
        _advertisingButton.onClick.RemoveListener(_controller.ShowAD);

        _controller.ShowWindow -= OnShow;
        _controller.HideWindow -= OnHide;
        _controller.ContinueGame -= OnConinueGame;
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

    private void OnShow(string description, LoseReason loseReason)
    {
        if(loseReason == LoseReason.NotEnoughCubes)
            _advertisingButton.gameObject.SetActive(false);

        _loseScreen.SetActive(true);
        _loseDecription.text = description;
        _audio.PlayBoomSound();
    }

    private void OnConinueGame()
    {
        _advertisingButton.gameObject.SetActive(false);
        OnHide();
    }
}
