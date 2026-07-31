using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PixelArtData", menuName = "Game/PixelArtData")]
public class PixelArtData : ScriptableObject
{
    [Header("Настройки пиксель-арта")]
    public string ArtName;
    public int Width = 10;
    public int Height = 10;
    public float PixelSize = 1f;
    public Vector3 StartPosition = Vector3.zero;

    [Header("Цветовая палитра")]
    public List<ColorCube> AvailableColors;

    [Header("Пиксели (расположение цветов)")]
    public List<PixelData> Pixels;

    [Header("Префабы")]
    public GameObject cubePrefab;
}

[System.Serializable]
public class PixelData
{
    public int X;
    public int Y;
    public ColorCube ColorPixel;

    public PixelData(int x, int y, ColorCube color)
    {
        this.X = x;
        this.Y = y;
        this.ColorPixel = color;
    }
}
