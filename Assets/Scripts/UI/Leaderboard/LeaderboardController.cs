using UnityEngine;
using YG;

public class LeaderboardController : MonoBehaviour
{
    [SerializeField] private LeaderboardView _view;
    [SerializeField] private ErrorWindowController _errorController;
    [SerializeField, TextArea] private string _errorTextRU;
    [SerializeField, TextArea] private string _errorTextEN;
    [SerializeField, TextArea] private string _errorTextTR;

    private void OnEnable() => _view.ClickOpenButton += TryOpenLeaderboard;

    private void OnDisable() => _view.ClickOpenButton -= TryOpenLeaderboard;

    private void TryOpenLeaderboard()
    {
        if (CheckAuth())
            _view.ShowLeaderboard();
        else
            _errorController.ShowError(Utils.GetTranslateText(_errorTextRU, _errorTextTR, _errorTextEN));
    }

    private bool CheckAuth() => YandexGame.auth;
}
