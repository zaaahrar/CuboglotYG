using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level Data")]
public class LevelDataSO : ScriptableObject 
{
    public int LevelNumber = 1;
    public List<CubeTypeData> CubeTypes = new List<CubeTypeData>();
    public int TotalCubes = 50;
    public int TotalBombs;
    public int TimeLimit = 60;
    public PixelArtData PixelArt;

    public void OnValidate() => TotalCubes = GetTotalCubes();

    private int GetTotalCubes()
    {
        int totalCubes = 0;

        for(int i = 0; i < CubeTypes.Count; i++)
            totalCubes += CubeTypes[i].CountCubes;

        return totalCubes;
    }
}

[System.Serializable]
public class CubeTypeData
{
    public ColorCube CurrentColor;
    public int CountCubes;
}

public enum ColorCube
{
    White,
    Black,
    Yellow,
    Blue,
    Red,
    Orange
}
