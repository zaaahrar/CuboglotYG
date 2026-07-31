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
}
