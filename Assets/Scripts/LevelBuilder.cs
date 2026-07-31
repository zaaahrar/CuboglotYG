using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private GameSettingsSO _gameSettings;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Transform _parentCubes;
    [SerializeField] private GameObject _ground;

    private List<Cube> _spawnedCubes = new List<Cube>();
    private List<Bomb> _spawnedBombs = new List<Bomb>();
    private List<Vector3> _spawnPositions = new List<Vector3>();
    private Bounds _groundBounds;

    public IEnumerator BuildingLevel(LevelDataSO levelData)
    {
        for(int i = 0; i < levelData.CubeTypes.Count; i++)
        {
            for(int j = 0; j < levelData.CubeTypes[i].CountCubes; j++)
            {
                yield return null;
                Vector3 spawnPosition = GetPositionSpawn();
                Cube cube = _spawner.SpawnCube(spawnPosition, _parentCubes);
                cube.SetColor(levelData.CubeTypes[i].CurrentColor);
                _spawnPositions.Add(spawnPosition);
                _spawnedCubes.Add(cube);
            }
        }

        for(int i = 0; i < levelData.TotalBombs; i++)
        {
            yield return null;
            Vector3 spawnPosition = GetPositionSpawn();
            _spawnedBombs.Add(_spawner.SpawnBomb(spawnPosition, _parentCubes));
            _spawnPositions.Add(spawnPosition);
        }
    }

    private Vector3 GetPositionSpawn()
    {
        if(_ground == null)
            throw new ArgumentNullException(nameof(_ground));

        Renderer groundRenderer = _ground.GetComponent<Renderer>();

        if(groundRenderer == null )
            throw new ArgumentNullException(nameof(groundRenderer));

        _groundBounds = groundRenderer.bounds;

        for (int attempt = 0; attempt < _gameSettings.MaxAttempts; attempt++)
        {
            Vector3 randomPosition = GetRandomPointOnGround();

            if (IsPositionValid(randomPosition))
                return randomPosition;
        }

        return GetRandomPointOnGround();
    }   

    private Vector3 GetRandomPointOnGround()
    {
        float offset = 1;

        Vector3 groundSize = _groundBounds.size;
        float randomX = UnityEngine.Random.Range(
            _groundBounds.min.x + offset,
            _groundBounds.max.x - offset);
        float randomZ = UnityEngine.Random.Range(
            _groundBounds.min.z + offset,
            _groundBounds.max.z - offset);

        float yPosition = 0.5f;

        return new Vector3(randomX, yPosition, randomZ);
    }

    private bool IsPositionValid(Vector3 position)
    {
        foreach (Vector3 existingPos in _spawnPositions)
        {
            float distance = Vector3.Distance(position, existingPos);

            if (distance < _gameSettings.MinDistanceBetweenCubes)
                return false;
        }

        return true;
    }
}
