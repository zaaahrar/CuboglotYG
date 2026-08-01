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

    private Timer _timer;

    private void OnDisable()
    {
        _cubeCounter.AllCubesCollected -= LoadPixelArtScene;
        _timer.TimerFinished -= LoadPixelArtScene;
    }

    public void Initialize(Timer timer)
    {
        if(timer == null)
            throw new ArgumentNullException(nameof(timer));

        _timer = timer;
        _cubeCounter.AllCubesCollected += LoadPixelArtScene;
        _timer.TimerFinished += LoadPixelArtScene;
    }

    public void LoadMainMenuScene() => SceneManager.LoadScene(MAIN_MENU_SCENE_INDEX);

    public void LoadGameplayScene() => SceneManager.LoadScene(GAMEPLAY_SCENE_INDEX);

    public void LoadPixelArtScene() => SceneManager.LoadScene(PIXEL_ART_SCENE_INDEX);
}
