using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using System;

public class SceneLoader : MonoBehaviour
{
    private const int MAIN_MENU_SCENE_INDEX = 0;
    private const int GAMEPLAY_SCENE_INDEX = 1;
    private const int PIXEL_ART_SCENE_INDEX = 2;

    [Inject] private CubeCollector _cubeCounter;

    private void OnDisable()
    {
        _cubeCounter.LevelComplete -= LoadPixelArtScene;
    }

    public void Initialize()
    {
        _cubeCounter.LevelComplete += LoadPixelArtScene;
    }

    public void LoadMainMenuScene() => SceneManager.LoadScene(MAIN_MENU_SCENE_INDEX);

    public void LoadGameplayScene() => SceneManager.LoadScene(GAMEPLAY_SCENE_INDEX);

    public void LoadPixelArtScene() => SceneManager.LoadScene(PIXEL_ART_SCENE_INDEX);
}
