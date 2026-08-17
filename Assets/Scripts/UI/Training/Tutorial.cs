using System;
using UnityEngine;
using YG;

public class Tutorial : MonoBehaviour
{
    public event Action OpenWindow;

    public void StartTutorial()
    {
        OpenWindow?.Invoke();
        Time.timeScale = 0;
        YandexGame.GameplayStop();
    }

    public void EndTraining()
    {
        Time.timeScale = 1f;
        YandexGame.savesData.IsTutorialCompleted = true;
        YandexGame.SaveProgress();
        YandexGame.GameplayStart();
    }
}
