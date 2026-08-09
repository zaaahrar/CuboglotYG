using System;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Zenject;

public class SettingsController : MonoBehaviour
{
    [Inject] private AudioController _audio;

    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Toggle _toggleVolume;
    [SerializeField] private Button _saveButton;

    private float _volumeGame;
    private bool _isSoundOn;

    private void OnEnable()
    {
        _saveButton.onClick.AddListener(SaveSettings);
        _isSoundOn = YandexGame.savesData.IsSoundOn;
        _volumeGame = YandexGame.savesData.VolumeGame;

        _volumeSlider.maxValue = 100;
        _volumeSlider.value = _volumeGame;
        _toggleVolume.isOn = _isSoundOn;
    }

    private void OnDisable() => _saveButton.onClick.RemoveListener(SaveSettings);

    private void SaveSettings()
    {
        YandexGame.savesData.VolumeGame = (int)_volumeSlider.value;
        YandexGame.savesData.IsSoundOn = _toggleVolume.isOn;
        YandexGame.SaveProgress();

        _audio.PlaySuccessfulActionSound();
        _audio.UpdateSettings(_toggleVolume.isOn, (int)_volumeSlider.value);
    }
}
