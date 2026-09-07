using UnityEngine;

public class ColorParser : MonoBehaviour
{
    [SerializeField] private Color _orange;
    [SerializeField] private Color _darkGreen;
    [SerializeField] private Color _brown;
    [SerializeField] private Color _pink;
    [SerializeField] private Color _darkPink;
    [SerializeField] private Color _darkBlue;
    [SerializeField] private Color _blue;

    public Color GetColor(ColorCube color)
    {
        switch (color)
        {
            case ColorCube.White:
                return Color.white;
            case ColorCube.Black:
                return Color.black;
            case ColorCube.Blue:
                return _blue;
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
            case ColorCube.Pink:
                return _pink;
            case ColorCube.DarkPink:
                return _darkPink;
            case ColorCube.DarkBlue:
                return _darkBlue;
            default:
                return Color.white;
        }
    }
}
