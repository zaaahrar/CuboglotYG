using UnityEngine;
using YG;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/Game")]
public class GameSettingsSO : ScriptableObject
{
    public float Speed;
    public float VictoryPercentage;

    [Header("Cube Falling")]
    public float LookDuration;
    public float MoveDuration;

    [Header("LevelBuilding")]
    public float MinDistanceBetweenCubes = 1f;
    public int MaxAttempts = 100;

    [Header("PixelArtBuilding")]
    public float PostBuildDelay;
    public float BlockPlacementDelay;
    public float ExplodeForce;
    public float Duration = 0.5f;
    public float PositionStrength = 0.3f;
    public int Vibrato = 10;

    [Header("AD")]
    public int LevelsBeforeAd = 3;

    [Header("Levels")]
    public LevelDataSO[] LevelsData;

    public LevelDataSO GetCurrentLevel() => LevelsData[YandexGame.savesData.CurrentLevelIndex];

    public void SetRandomLevel()
    {
        int newLevel;

        do
            newLevel = Random.Range(0, LevelsData.Length);
        while(newLevel == YandexGame.savesData.CurrentLevelIndex);

        YandexGame.savesData.CurrentLevelIndex = 18;
        YandexGame.SaveProgress();
    }
}
