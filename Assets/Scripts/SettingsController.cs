using UnityEngine;
using YG;
using Zenject;

public class SettingsController : MonoBehaviour
{
    [Inject] private AudioController _audio;
    private LanguageChanger _languageChanger;

    private void Start() => _languageChanger = new LanguageChanger();

    public void SaveSoundSettings(int musicVolume, int effectsVolume, bool isSoundOn)
    {
        var data = YandexGame.savesData;

        if (data.MusicVolume == musicVolume && data.EffectsVolume == effectsVolume && data.IsSoundOn == isSoundOn)
            return;

        YandexGame.savesData.MusicVolume = musicVolume;
        YandexGame.savesData.EffectsVolume = effectsVolume;
        YandexGame.savesData.IsSoundOn = isSoundOn;
        YandexGame.SaveProgress();

        _audio.UpdateSettings(isSoundOn, musicVolume, effectsVolume);
    }

    public void SaveLanguageSettings(string lang, bool isAuto)
    {
        var data = YandexGame.savesData;

        if (data.language == lang && data.IsAutoLanguage == isAuto)
            return;

        YandexGame.savesData.language = lang;
        YandexGame.savesData.IsAutoLanguage = isAuto;
        YandexGame.SaveProgress();
        _languageChanger.SwitchLanguage(lang);
    }
}
