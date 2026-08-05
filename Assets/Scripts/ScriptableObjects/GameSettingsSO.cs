using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/Game")]
public class GameSettingsSO : ScriptableObject
{
    public string INDEX_LEVEL_DATA_KEY = "IndexLevelData";

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

    [Header("Levels")]
    public LevelDataSO[] LevelsData;

    public LevelDataSO GetLevel()
    {
        if (PlayerPrefs.HasKey(INDEX_LEVEL_DATA_KEY))
            return LevelsData[PlayerPrefs.GetInt(INDEX_LEVEL_DATA_KEY)];

        SetRandomLevel();
        return LevelsData[PlayerPrefs.GetInt(INDEX_LEVEL_DATA_KEY)];
    }

    public void SetRandomLevel() => PlayerPrefs.SetInt(INDEX_LEVEL_DATA_KEY, Random.Range(0, LevelsData.Length - 1));
}
