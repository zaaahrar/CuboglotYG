using UnityEngine;
using Zenject;

public class MainMenuController : MonoBehaviour
{
    [Inject] private GameSettingsSO _gameSettingsSO;
    [Inject] private SceneLoader _sceneLoader;

    public void StartGame()
    {
        if(PlayerPrefs.HasKey(SaveDataKeys.IndelLevelData))
            PlayerPrefs.DeleteKey(SaveDataKeys.IndelLevelData);

        _gameSettingsSO.SetRandomLevel();
        _sceneLoader.LoadGameplayScene();
    }
}
