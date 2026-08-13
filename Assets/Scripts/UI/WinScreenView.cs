using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Zenject;

public class WinScreenView : MonoBehaviour
{
    [Inject] private SceneLoader _sceneLoader;
    [Inject] private GameSettingsSO _gameSettings;
    [Inject] private AudioController _audioController;

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private LevelDataSO _levelData;
    [SerializeField] private WinController _controller;
    [SerializeField] private ErrorWindowController _errorController;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private TMP_Text _collectCubesText;
    [SerializeField] private TMP_Text _goldText;
    [SerializeField] private Button _exitInMenuButton;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _advertisingButton;
    [SerializeField] private GameObject[] _stars;

    [Header("Animation")]
    [SerializeField] private float _showDuration;

    private float _yStartPosition = 1300;
    private string _descriptionError = "Неудачное воспроизведение рекламы";

    private void Start()
    {
        _controller.UpdateWinScreen += OnUpdateScreen;
        _controller.ShowWinScreen += OnShow;
        YandexGame.ErrorVideoEvent += OnErrorVideo;
        _controller.SuccessfulAdvertising += OnSuccessfulAdvertising;

        _exitInMenuButton.onClick.AddListener(_sceneLoader.LoadMainMenuScene);
        _nextLevelButton.onClick.AddListener(OnNextLevel);
        _advertisingButton.onClick.AddListener(_controller.ShowAD);

        _winScreen.SetActive(false);

        for (int i = 0; i < _stars.Length; i++)
            _stars[i].gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _controller.UpdateWinScreen -= OnUpdateScreen;
        _controller.ShowWinScreen -= OnShow;
        YandexGame.ErrorVideoEvent -= OnErrorVideo;
        _controller.SuccessfulAdvertising -= OnSuccessfulAdvertising;

        _exitInMenuButton.onClick.RemoveListener(_sceneLoader.LoadMainMenuScene);
        _nextLevelButton.onClick.RemoveListener(OnNextLevel);
        _advertisingButton.onClick.AddListener(_controller.ShowAD);
    }

    private void OnUpdateScreen(int cubesCollect, int totalCubes, int gold, int stars)
    {
        _collectCubesText.text = $"{cubesCollect}/{totalCubes}";

        if (stars != 0)
        {
            for(int i = 0; i < stars; i++)
                _stars[i].gameObject.SetActive(true);
        }

        _goldText.text = gold.ToString();

    }

    private void OnNextLevel()
    {
        _audioController.DisableAllSounds();
        _gameSettings.SetRandomLevel();
        _sceneLoader.LoadGameplayScene();
    }

    private void OnShow()
    {
        _rectTransform.anchoredPosition = new Vector2(0, _yStartPosition);
        _winScreen.SetActive(true);
        _audioController.PlayWinSound();
        _rectTransform.DOAnchorPosY(0, _showDuration);
    }

    private void OnErrorVideo() => _errorController.ShowError(_descriptionError);

    private void OnSuccessfulAdvertising()
    {
        _advertisingButton.gameObject.SetActive(false);
        _audioController.PlaySuccessfulAdvertisingSound();
    }
}
