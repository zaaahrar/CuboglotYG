using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WinScreenView : MonoBehaviour
{
    [Inject] private SceneLoader _sceneLoader;

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private LevelDataSO _levelData;
    [SerializeField] private WinController _controller;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private TMP_Text _collectCubesText;
    [SerializeField] private TMP_Text _goldText;
    [SerializeField] private Button _exitInMenuButton;
    [SerializeField] private Button _nextLevelButton;

    [Header("Animation")]
    [SerializeField] private float _showDuration;

    private float _yStartPosition = 1300;

    private void Start()
    {
        _winScreen.SetActive(false);
        _controller.UpdateWinScreen += OnUpdateScreen;
        _controller.ShowWinScreen += OnShow;
        _exitInMenuButton.onClick.AddListener(_sceneLoader.LoadMainMenuScene);
        _nextLevelButton.onClick.AddListener(_sceneLoader.LoadGameplayScene);
    }

    private void OnDisable()
    {
        _controller.UpdateWinScreen -= OnUpdateScreen;
        _controller.ShowWinScreen -= OnShow;
        _exitInMenuButton.onClick.RemoveListener(_sceneLoader.LoadMainMenuScene);
        _nextLevelButton.onClick.RemoveListener(_sceneLoader.LoadGameplayScene);
    }

    private void OnUpdateScreen(int cubesCollect, int gold)
    {
        _collectCubesText.text = $"{cubesCollect}/{_levelData.TotalCubes}";
        _goldText.text = gold.ToString();
    }

    private void OnShow()
    {
        _rectTransform.anchoredPosition = new Vector2(0, _yStartPosition);
        _winScreen.SetActive(true);
        _rectTransform.DOAnchorPosY(0, _showDuration);
    }
}
