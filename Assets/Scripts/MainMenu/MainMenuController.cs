using UnityEngine;
using Zenject;

public class MainMenuController : MonoBehaviour
{
    [Inject] private GameSettingsSO _gameSettingsSO;
    [Inject] private SceneLoader _sceneLoader;

    public void StartGame()
    {
        if(PlayerPrefs.HasKey(_gameSettingsSO.INDEX_LEVEL_DATA_KEY))
            PlayerPrefs.DeleteKey(_gameSettingsSO.INDEX_LEVEL_DATA_KEY);
        _gameSettingsSO.SetRandomLevel();
        _sceneLoader.LoadGameplayScene();
    }
}
