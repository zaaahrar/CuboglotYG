using UnityEngine;
using YG;
using Zenject;

public class MainMenuController : MonoBehaviour
{
    [Inject] private GameSettingsSO _gameSettingsSO;
    [Inject] private SceneLoader _sceneLoader;
    [Inject] private AudioController _audioController;
    [SerializeField] private AudioClip _menuThem;

    private LanguageChanger _languageChanger;

    private void Start()
    {
        _languageChanger = new LanguageChanger();
        _audioController.PlaySceneThem(_menuThem);
        SetLanguage();
        YandexGame.GameplayStop();
    }

    public void StartGame()
    {
        _audioController.PlayClickSound();
        _gameSettingsSO.SetRandomLevel();
        _audioController.DisableAllSounds();
        _sceneLoader.LoadGameplayScene();
    }

    public void SetLanguage()
    {
        if (YandexGame.savesData.IsAutoLanguage)
            _languageChanger.SwitchLanguage(YandexGame.EnvironmentData.language);
        else
            _languageChanger.SwitchLanguage(YandexGame.savesData.language);
    }
}
