using UnityEngine;
using UnityEngine.UI;
using YG;
using Zenject;

public class SettingsScreenView : MonoBehaviour
{
    [Inject] private AudioController _audio;
    [SerializeField] private SettingsController _controller;
    [SerializeField] private GameObject _window;
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _saveButton;

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _effectsSlider;
    [SerializeField] private Toggle _toggleVolume;

    private float _maxVolume = 100;

    private void OnEnable()
    {
        _saveButton.onClick.AddListener(OnSaveSettings);
        _openButton.onClick.AddListener(Show);
        _closeButton.onClick.AddListener(Hide);

        _musicSlider.maxValue = _maxVolume;
        _musicSlider.value = YandexGame.savesData.MusicVolume;
        _effectsSlider.maxValue = _maxVolume;
        _effectsSlider.value = YandexGame.savesData.EffectsVolume;
        _toggleVolume.isOn = YandexGame.savesData.IsSoundOn;

        _window.SetActive(false);
    }

    private void OnDisable()
    {
        _saveButton.onClick.RemoveListener(OnSaveSettings);
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

    private void OnSaveSettings() => _controller.SaveSettings((int)_musicSlider.value, (int)_effectsSlider.value, _toggleVolume.isOn);
}
