using System;
using UnityEngine;
using UnityEngine.UI;
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

        if (!PlayerPrefs.HasKey(SaveDataKeys.VolumeGame))
            _volumeGame = 100;
        else
            _volumeGame = PlayerPrefs.GetInt(SaveDataKeys.VolumeGame);

        if (!PlayerPrefs.HasKey(SaveDataKeys.IsSoundOn))
            _isSoundOn = true;
        else
            _isSoundOn = Convert.ToBoolean(PlayerPrefs.GetInt(SaveDataKeys.IsSoundOn));

        _volumeSlider.maxValue = 100;
        _volumeSlider.value = _volumeGame;
        _toggleVolume.isOn = _isSoundOn;
    }

    private void OnDisable()
    {
        _saveButton.onClick.RemoveListener(SaveSettings);
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt(SaveDataKeys.VolumeGame, (int)_volumeSlider.value);
        PlayerPrefs.SetInt(SaveDataKeys.IsSoundOn, Convert.ToInt32(_toggleVolume.isOn));
        _audio.PlaySuccessfulActionSound();
        _audio.UpdateSettings(_toggleVolume.isOn, (int)_volumeSlider.value);
    }
}
