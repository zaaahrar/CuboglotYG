using UnityEngine;
using Zenject;

public class PauseController : MonoBehaviour
{
    [Inject] private SceneLoader _sceneLoader;
    [SerializeField] private SettingsScreenView _settingsView;

    public void ExitInMenu()
    {
        Time.timeScale = 1;
        _sceneLoader.LoadMainMenuScene();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        _sceneLoader.LoadGameplayScene();
    }
}
