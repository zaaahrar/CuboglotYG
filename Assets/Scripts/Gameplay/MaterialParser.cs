using UnityEngine;

public class MaterialParser : MonoBehaviour
{
    [SerializeField] private Material _white;
    [SerializeField] private Material _black;
    [SerializeField] private Material _blue;
    [SerializeField] private Material _yellow;
    [SerializeField] private Material _red;
    [SerializeField] private Material _orange;
    [SerializeField] private Material _gray;
    [SerializeField] private Material _green;
    [SerializeField] private Material _darkGreen;
    [SerializeField] private Material _brown;
    [SerializeField] private Material _pink;
    [SerializeField] private Material _darkPink;
    [SerializeField] private Material _darkBlue;

    public Material GetMaterial(ColorCube color)
    {
        switch (color)
        {
            case ColorCube.Black:
                return _black;
            case ColorCube.Blue:
                return _blue;
            case ColorCube.Yellow:
                return _yellow;
            case ColorCube.Red:
                return _red;
            case ColorCube.Orange:
                return _orange;
            case ColorCube.Gray:
                return _gray;
            case ColorCube.Green:
                return _green;
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
                return _white;
        }
    }
}
