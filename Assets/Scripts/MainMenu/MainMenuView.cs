using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private MainMenuController _controller;

    private void Start()
    {
        _startButton.onClick.AddListener(_controller.StartGame);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(_controller.StartGame);
    }
}
