using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System;

public class LeaderboardView : MonoBehaviour
{
    [Inject] private AudioController _audio;

    [SerializeField] private LeaderboardController _controller;
    [SerializeField] private GameObject _infoLeaderboardWindow;
    [SerializeField] private GameObject _leaderboardWindow;

    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _openInfoButton;
    [SerializeField] private Button _closeInfoButton;

    public event Action ClickOpenButton;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnHide);
        _openButton.onClick.AddListener(OnClickOpenButton);
        _openInfoButton.onClick.AddListener(OnShowInfo);
        _closeInfoButton.onClick.AddListener(OnHideInfo);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnHide);
        _openButton.onClick.RemoveListener(OnClickOpenButton);
        _openInfoButton.onClick.RemoveListener(OnShowInfo);
        _closeInfoButton.onClick.RemoveListener(OnHideInfo);
    }

    private void OnClickOpenButton() => ClickOpenButton?.Invoke();

    public void ShowLeaderboard()
    {   
        _audio.PlayClickSound();
        _leaderboardWindow.SetActive(true);
    }

    private void OnHide()
    {
        _audio.PlayClickSound();
        _leaderboardWindow.SetActive(false);
    }

    private void OnShowInfo()
    {
        _audio.PlayClickSound();
        _infoLeaderboardWindow.SetActive(true);
    }

    private void OnHideInfo()
    {
        _audio.PlayClickSound();
        _infoLeaderboardWindow.SetActive(false);
    }
}
