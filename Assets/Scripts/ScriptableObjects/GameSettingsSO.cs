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

    [Header("HoleGrowth")]
    public int[] CubesToNextGrowth = { 3, 9, 15 };
    public int[] CapacitysPlayer = { 1, 3, 5, 10 };
    public float DurationGrow = 0.5f;
    public float GrowthStep = 0.2f;

    [Header("LevelBuilding")]
    public float MinDistanceBetweenCubes = 1f;
    public float MinDistanceFromPlayer = 2f;
    public int MaxAttempts = 100;
    public int[] Denomiantions = { 10, 5, 3, 1 };
    public float BaseScale = 0.75f;
    public float ScalePerUnit = 0.1f;
    public int MinCubesPerDenomination = 3;
    public Vector3 SpawnPositionPlayer = new Vector3(0, 5, 0);

    [Header("Рандомизция количества кубов")]
    [Tooltip("Минимальная доля кубов от максимума (0.5 = половина)")]
    [Range(0, 1)]
    public float MinFriction = 0.4f;
    [Tooltip("Максимальная доля кубов от максимума")]
    [Range(0, 1)]
    public float MaxFriction = 0.9f;

    [Header("PixelArtBuilding")]
    public float PostBuildDelay;
    public float BlockPlacementDelay;
    public float ExplodeForce;
    public float DurationBuildPixel = 0.5f;
    public float PositionStrength = 0.3f;
    public int Vibrato = 10;

    [Header("AD")]
    public int LevelsBeforeAd = 3;

    [Header("Levels")]
    public LevelDataSO[] LevelsData;
    [Header("Skins")]
    public Material[] Skins;

    public LevelDataSO GetCurrentLevel() => LevelsData[YandexGame.savesData.CurrentLevelIndex];

    public void SetRandomLevel()
    {
        int newLevel;

        do
            newLevel = Random.Range(0, LevelsData.Length);
        while(newLevel == YandexGame.savesData.CurrentLevelIndex);

        YandexGame.savesData.CurrentLevelIndex = newLevel;
        YandexGame.SaveProgress();
    }
}
