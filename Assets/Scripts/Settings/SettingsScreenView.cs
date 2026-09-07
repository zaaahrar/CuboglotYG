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

    [SerializeField] private LangToggle[] _langsToggles;

    private float _maxVolume = 100;
    private bool _isAuto;
    private string _currentLang;

    private void OnEnable()
    {
        _saveButton.onClick.AddListener(OnSaveSettings);
        _openButton.onClick.AddListener(Show);
        _closeButton.onClick.AddListener(Hide);    
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
        UpdateUI();
        _audio.PlayClickSound();
    }

    public void Hide()
    {
        _window.SetActive(false);
        _audio.PlayClickSound();
    }

    private void SetLanguageToggle()
    {
        _currentLang = YandexGame.savesData.language;
        _isAuto = YandexGame.savesData.IsAutoLanguage;

        if (_langsToggles.Length > 0)
        {
            foreach(var langToggle in _langsToggles)
            {
                if(_isAuto && langToggle.LanguageName == LanguageConstants.LanguageAuto)
                {
                    langToggle.OnToggle();
                    return;
                }

                if(!_isAuto && langToggle.LanguageName == _currentLang)
                {
                    langToggle.OnToggle();
                    return;
                }
            }
        }
    }

    private void UpdateUI()
    {
        _musicSlider.maxValue = _maxVolume;
        _musicSlider.value = YandexGame.savesData.MusicVolume;
        _effectsSlider.maxValue = _maxVolume;
        _effectsSlider.value = YandexGame.savesData.EffectsVolume;
        _toggleVolume.isOn = YandexGame.savesData.IsSoundOn;
        SetLanguageToggle();
    }

    private void OnSaveSettings()
    {
        _controller.SaveSoundSettings((int)_musicSlider.value, (int)_effectsSlider.value, _toggleVolume.isOn);
        _controller.SaveLanguageSettings(GetLang(), TryGetAutoState());
        _audio.PlaySuccessfulActionSound();
    }

    private string GetLang()
    {
        if (_langsToggles.Length > 0)
        {
            foreach (var langToggle in _langsToggles)
            {
                if (langToggle.IsOn && langToggle.LanguageName != LanguageConstants.LanguageAuto) 
                    return langToggle.LanguageName;
            }
        }

        return YandexGame.EnvironmentData.language;
    }

    private bool TryGetAutoState()
    {
        if (_langsToggles.Length > 0)
        {
            foreach (var langToggle in _langsToggles)
            {
                if (langToggle.IsOn && langToggle.LanguageName == LanguageConstants.LanguageAuto)
                    return true;
            }
        }

        return false;
    }
}
