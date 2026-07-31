using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CubeCounter : MonoBehaviour
{
    private const int ScenePixelArtBuilding = 2;

    [SerializeField] private FallObjectsTriggerCheker _objectsCheker;

    public event Action<int, int> UpdateCubesCounter;

    private LevelDataSO _currentLevelData;
    private List<ColorCube> _collectCubeColors;
    private int _countCubes = 0;

    public int CountCubes
    {
        get => _countCubes;
        private set
        {
            _countCubes = value;
            UpdateCubesCounter?.Invoke(_countCubes, _currentLevelData.TotalCubes);
        }
    }

    public void Initialize(LevelDataSO levelData)
    {
        if(levelData == null)
            throw new ArgumentNullException(nameof(levelData));

        _currentLevelData = levelData;
        CountCubes = 0;
        _objectsCheker.CollectCube += OnCollectCube;
        _collectCubeColors = new List<ColorCube>();

        DontDestroyOnLoad(gameObject);
    }

    private void OnDisable()
    {
        _objectsCheker.CollectCube -= OnCollectCube;
    }

    private void OnCollectCube(Cube cube)
    {
        if(cube == null)
            throw new ArgumentNullException(nameof(cube));

        CountCubes++;
        _collectCubeColors.Add(cube.CurrentColor);

        if (CountCubes == _currentLevelData.TotalCubes)
            SceneManager.LoadScene(ScenePixelArtBuilding);
    }
}
