using UnityEngine;
using Zenject;

public class MainMenuController : MonoBehaviour
{
    [Inject] private GameSettingsSO _gameSettingsSO;
    [Inject] private SceneLoader _sceneLoader;
    [Inject] private AudioController _audioController;

    [SerializeField] private AudioClip _menuThem;

    private void Start()
    {
        _audioController.PlaySceneThem(_menuThem);
    }

    public void StartGame()
    {
        _audioController.PlayClickSound();
        _gameSettingsSO.SetRandomLevel();
        _audioController.DisableAllSounds();
        _sceneLoader.LoadGameplayScene();
    }
}
