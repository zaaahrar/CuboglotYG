using UnityEngine;
using Zenject;
using System.Collections;
using System;
using System.Collections.Generic;
using DG.Tweening;

public class PixelArtBuilder : MonoBehaviour
{
    [Inject] private CubeCollector _cubeCounter;
    [Inject] private GameSettingsSO _settings;
    [Inject] private AudioController _audio;

    [SerializeField] private LevelDataSO _levelData;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Transform _parentCubes;
    [SerializeField] private WinController _winController;

    private List<Cube> _builtCubes = new List<Cube>();
    private bool _isBuilding = false;
    private WaitForSeconds _postBuildDelay;
    private WaitForSeconds _blockPlacementDelay;

    private void Start()
    {
        if (_settings == null)
            throw new ArgumentException();

        StartCoroutine(BuildingPixelArt());
        _postBuildDelay = new WaitForSeconds(_settings.PostBuildDelay);
        _blockPlacementDelay = new WaitForSeconds(_settings.BlockPlacementDelay);
    }

    private IEnumerator BuildingPixelArt()
    {
        _isBuilding = true;
        PixelArtData pixelArt = _levelData.PixelArt;

        if(_cubeCounter.CurrentCubeCount > 0)
        {
            for (int i = 0; i < pixelArt.Pixels.Count; i++)
            {
                var colorList = _cubeCounter.CollectCubeColors;

                foreach (var color in colorList)
                {
                    if (pixelArt.Pixels[i].ColorPixel == color)
                    {
                        yield return _blockPlacementDelay;
                        float xPosition = pixelArt.Pixels[i].X * pixelArt.PixelSize;
                        float yPosition = pixelArt.Pixels[i].Y * pixelArt.PixelSize;
                        Vector3 position = new Vector3(xPosition, yPosition, _parentCubes.transform.position.z);
                        Cube cube = _spawner.SpawnCube(position, _parentCubes);
                        cube.SetColor(color);
                        cube.name = i.ToString();
                        cube.SetKinematic(_isBuilding);
                        _audio.PlayCollectSound();
                        cube.transform.DOShakePosition(_settings.Duration, _settings.PositionStrength, _settings.Vibrato)
                            .SetEase(Ease.OutQuad).SetLink(cube.gameObject, LinkBehaviour.KillOnDisable);
                        _builtCubes.Add(cube);
                        _cubeCounter.RemoveColor(color);
                        break;
                    }
                }
            }
        }

        yield return _postBuildDelay;

        _isBuilding = false;
        _winController.Win(_cubeCounter.CurrentCubeCount);

        foreach (var cube in _builtCubes)
        {
            cube.SetKinematic(_isBuilding);
            cube.Explode();
        }
    }

    [ContextMenu("GetColors")]
    public void GetColors()
    {
        int red = 0;
        int black = 0;
        int white = 0;

        foreach (var pixel in _levelData.PixelArt.Pixels)
        {

            if (pixel.ColorPixel == ColorCube.Red)
                red++;
            if (pixel.ColorPixel == ColorCube.Black)
                black++;
            if (pixel.ColorPixel == ColorCube.White)
                white++;
        }

        Debug.Log($"red: {red}, black: {black}, white: {white}");
    }
}
