using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingsScreenView : MonoBehaviour
{
    [SerializeField] private GameObject _window;
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;
    [Inject] private AudioController _audio;

    private void Start()
    {
        _window.SetActive(false);
        _openButton.onClick.AddListener(Show);
        _closeButton.onClick.AddListener(Hide);
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(Show);
        _closeButton.onClick.RemoveListener(Hide);
    }

    public void Show()
    {
        _window.SetActive(true);
        _audio.PlayClickSound();
    }

    public void Hide()
    {
        _window.SetActive(false);
        _audio.PlayClickSound();
    }
}
