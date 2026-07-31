using UnityEngine;

public class ColorParser : MonoBehaviour
{
    [SerializeField] private Color _orange;

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
            default:
                return Color.white;
        }
    }
}
