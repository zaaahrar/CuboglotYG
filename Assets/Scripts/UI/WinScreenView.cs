using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WinScreenView : MonoBehaviour
{
    [Inject] private SceneLoader _sceneLoader;
    [Inject] private AudioController _audioController;

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private LevelDataSO _levelData;
    [SerializeField] private WinController _controller;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private TMP_Text _collectCubesText;
    [SerializeField] private TMP_Text _goldText;
    [SerializeField] private Button _exitInMenuButton;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private GameObject[] _stars;

    [Header("Animation")]
    [SerializeField] private float _showDuration;

    private float _yStartPosition = 1300;

    private void Start()
    {
        _winScreen.SetActive(false);
        _controller.UpdateWinScreen += OnUpdateScreen;
        _controller.ShowWinScreen += OnShow;
        _exitInMenuButton.onClick.AddListener(_sceneLoader.LoadMainMenuScene);
        _nextLevelButton.onClick.AddListener(OnNextLevel);

        for (int i = 0; i < _stars.Length; i++)
            _stars[i].gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _controller.UpdateWinScreen -= OnUpdateScreen;
        _controller.ShowWinScreen -= OnShow;
        _exitInMenuButton.onClick.RemoveListener(_sceneLoader.LoadMainMenuScene);
        _nextLevelButton.onClick.RemoveListener(OnNextLevel);
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
        _sceneLoader.LoadGameplayScene();
    }

    private void OnShow()
    {
        _rectTransform.anchoredPosition = new Vector2(0, _yStartPosition);
        _winScreen.SetActive(true);
        _audioController.PlayWinSound();
        _rectTransform.DOAnchorPosY(0, _showDuration);
    }
}
