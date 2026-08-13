using UnityEngine;
using YG;
using Zenject;

public class SettingsController : MonoBehaviour
{
    [Inject] private AudioController _audio;

    public void SaveSettings(int musicVolume, int effectsVolume, bool isSoundOn)
    {
        YandexGame.savesData.MusicVolume = musicVolume;
        YandexGame.savesData.EffectsVolume = effectsVolume;
        YandexGame.savesData.IsSoundOn = isSoundOn;
        YandexGame.SaveProgress();

        _audio.PlaySuccessfulActionSound();
        _audio.UpdateSettings(isSoundOn, musicVolume, effectsVolume);
    }
}
