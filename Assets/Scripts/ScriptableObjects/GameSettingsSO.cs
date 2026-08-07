using UnityEngine;

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

    [Header("Levels")]
    public LevelDataSO[] LevelsData;

    public LevelDataSO GetLevel()
    {
        if (PlayerPrefs.HasKey(SaveDataKeys.IndelLevelData))
            return LevelsData[PlayerPrefs.GetInt(SaveDataKeys.IndelLevelData)];

        SetRandomLevel();
        return LevelsData[PlayerPrefs.GetInt(SaveDataKeys.IndelLevelData)];
    }

    public void SetRandomLevel() => PlayerPrefs.SetInt(SaveDataKeys.IndelLevelData, Random.Range(0, LevelsData.Length - 1));
}
