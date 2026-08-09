using UnityEngine;
using YG;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/Game")]
public class GameSettingsSO : ScriptableObject
{
    public float Speed;

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

    [Header("Levels")]
    public LevelDataSO[] LevelsData;

    public LevelDataSO GetCurrentLevelLevel() => LevelsData[YandexGame.savesData.CurrentLevelIndex];

    public void SetRandomLevel()
    {
        YandexGame.savesData.CurrentLevelIndex = Random.Range(0, LevelsData.Length - 1);
        YandexGame.SaveProgress();
    }
}
