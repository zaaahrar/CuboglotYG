using UnityEngine;
using YG;

public class LeaderboardController : MonoBehaviour
{
    [SerializeField] private LeaderboardView _view;
    [SerializeField] private ErrorWindowController _errorController;
    [SerializeField, TextArea] private string _errorText;

    private void OnEnable() => _view.ClickOpenButton += TryOpenLeaderboard;

    private void OnDisable() => _view.ClickOpenButton -= TryOpenLeaderboard;

    private void TryOpenLeaderboard()
    {
        if (CheckAuth())
            _view.ShowLeaderboard();
        else
            _errorController.ShowError(_errorText);
    }

    private bool CheckAuth() => YandexGame.auth;
}
