using UnityEngine;

public class ColorParser : MonoBehaviour
{
    [SerializeField] private Color _orange;
    [SerializeField] private Color _darkGreen;
    [SerializeField] private Color _brown;

    public Color GetColor(ColorCube color)
    {
        switch (color)
        {
            case ColorCube.White:
                return Color.white;
            case ColorCube.Black:
                return Color.black;
            case ColorCube.Blue:
                return Color.blue;
            case ColorCube.Yellow:
                return Color.yellow;
            case ColorCube.Red:
                return Color.red;
            case ColorCube.Orange:
                return _orange;
            case ColorCube.Gray:
                return Color.grey;
            case ColorCube.Green:
                return Color.green;
            case ColorCube.DarkGreen:
                return _darkGreen;
            case ColorCube.Brown:
                return _brown;
            default:
                return Color.white;
        }
    }
}
