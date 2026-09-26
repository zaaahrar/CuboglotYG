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

        _levelData = _settings.GetCurrentLevel();
        StartCoroutine(BuildingPixelArt());
        _postBuildDelay = new WaitForSeconds(_settings.PostBuildDelay);
        _blockPlacementDelay = new WaitForSeconds(_settings.BlockPlacementDelay);
    }

    private IEnumerator BuildingPixelArt()
    {
        _isBuilding = true;
        PixelArtData pixelArt = _levelData.PixelArt;

        if (_cubeCounter.CurrentCubeCount > 0)
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

                        Vector3 targetScale = cube.transform.localScale;
                        cube.transform.localScale = Vector3.zero;

                        cube.transform.rotation = Quaternion.Euler(
                            UnityEngine.Random.Range(-15f, 15f),
                            UnityEngine.Random.Range(-15f, 15f),
                            UnityEngine.Random.Range(-15f, 15f)
                        );

                        Vector3 startPos = position + Vector3.down * 0.5f;
                        cube.transform.position = startPos;

                        Sequence appearSequence = DOTween.Sequence();

                        appearSequence.Append(
                            cube.transform.DOMove(position, 0.35f)
                                .SetEase(Ease.OutBack)
                        );

                        appearSequence.Join(
                            cube.transform.DOScale(targetScale, 0.35f)
                                .SetEase(Ease.OutBack)
                        );

                        appearSequence.Join(
                            cube.transform.DORotateQuaternion(Quaternion.identity, 0.35f)
                                .SetEase(Ease.OutQuad)
                        );

                        appearSequence.Append(
                            cube.transform.DOShakePosition(
                                    _settings.Duration * 0.5f,
                                    _settings.PositionStrength * 0.5f,
                                    _settings.Vibrato)
                                .SetEase(Ease.OutQuad)
                        );

                        appearSequence.SetLink(cube.gameObject, LinkBehaviour.KillOnDisable);


                        _audio.PlayCollectSound();
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
        int green = 0;
        int darkGreen = 0;
        int black = 0;
        int white = 0;
        int gray = 0;
        int brown = 0;
        int yellow = 0;
        int red = 0;
        int orange = 0;
        int pink = 0;
        int darkPink = 0;
        int darkBlue = 0;
        int blue = 0;

        foreach (var pixel in _levelData.PixelArt.Pixels)
        {

            if (pixel.ColorPixel == ColorCube.Green)
                green++;
            if (pixel.ColorPixel == ColorCube.Black)
                black++;
            if (pixel.ColorPixel == ColorCube.White)
                white++;
            if (pixel.ColorPixel == ColorCube.DarkGreen)
                darkGreen++;
            if (pixel.ColorPixel == ColorCube.Gray)
                gray++;
            if (pixel.ColorPixel == ColorCube.Brown)
                brown++;
            if (pixel.ColorPixel == ColorCube.Yellow)
                yellow++;
            if (pixel.ColorPixel == ColorCube.Red)
                red++;
            if (pixel.ColorPixel == ColorCube.Orange)
                orange++;
            if (pixel.ColorPixel == ColorCube.Pink)
                pink++;
            if (pixel.ColorPixel == ColorCube.DarkPink)
                darkPink++;
            if (pixel.ColorPixel == ColorCube.DarkBlue)
                darkBlue++;
            if (pixel.ColorPixel == ColorCube.Blue)
                blue++;
        }

        Debug.Log($"green: {green}, black: {black}, white: {white}," +
            $" darkGreen: {darkGreen}, gray: {gray}, brown: {brown}, yellow: {yellow}," +
            $"red: {red}, orange: {orange}, pink: {pink}, darkPink: {darkPink}, darkBlue: {darkBlue}," +
            $"blue: {blue}");
        Debug.Log($"total: {green + darkGreen + black + white + gray + brown + yellow + red + orange + pink + darkPink + darkBlue + blue}");
    }
}
