using UnityEngine;
using System;
using Zenject;

public class Cube : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [Inject] private ColorParser _colorParser;

    private Material _material;
    public ColorCube CurrentColor { get; private set; }

    public void Initialize()
    {
        _material = _renderer.material;
    }

    public void SetColor(ColorCube color)
    {
        if (_material == null)
            throw new ArgumentNullException(nameof(_material));

        if (_colorParser == null)
            Debug.Log("Null");

        CurrentColor = color;
        _material.color = _colorParser.GetColor(CurrentColor);
    }
}
