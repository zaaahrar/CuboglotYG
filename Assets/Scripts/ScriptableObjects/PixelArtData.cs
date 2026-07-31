using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PixelArtData", menuName = "Game/PixelArtData")]
public class PixelArtData : ScriptableObject
{
    [Header("Настройки пиксель-арта")]
    public string ArtName;
    public float PixelSize;

    [Header("Пиксели (расположение цветов)")]
    public List<PixelData> Pixels;
}

[System.Serializable]
public class PixelData
{
    public float X;
    public float Y;
    public ColorCube ColorPixel;
}
