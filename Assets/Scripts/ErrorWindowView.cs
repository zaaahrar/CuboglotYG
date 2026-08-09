using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ErrorWindowView : MonoBehaviour
{
    [SerializeField] private GameObject _window;
    [SerializeField] private Button _closeButton;
    [SerializeField] private ErrorWindowController _controller;
    [SerializeField] private TMP_Text _descriptionText;

    [Inject] private AudioController _audio;

    private void Start()
    {
        _closeButton.onClick.AddListener(Hide);
        _controller.ShowErrorWindow += Show;
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(Hide);
        _controller.ShowErrorWindow -= Show;
    }

    private void Show(string description)
    {
        _audio.PlayClickSound();
        _descriptionText.text = description;
        _window.SetActive(true);
    }

    private void Hide()
    {
        _audio.PlayClickSound();
        _window.SetActive(false);
    }
}
