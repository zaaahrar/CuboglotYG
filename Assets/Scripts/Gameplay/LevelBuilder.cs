using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelBuilder : MonoBehaviour
{
    [Inject] private GameSettingsSO _gameSettings;
 
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Transform _parentCubes;
    [SerializeField] private GameObject _ground;
    [SerializeField] private CinemachineVirtualCamera _camera;

    [Header("Spawn Settings")]
    [SerializeField] private float _spawnYPosition = 0.5f;
    [SerializeField] private float _spawnOffsetFromEdge = 1.5f;
    [SerializeField] private Collider _groundCollider;
    [SerializeField] private float _raycastHeight = 50f;

    private List<Cube> _spawnedCubes = new List<Cube>();
    private List<Bomb> _spawnedBombs = new List<Bomb>();
    private List<Vector3> _spawnPositions = new List<Vector3>();
    private Bounds _groundBounds;
    private float _bombSpawnHeight = 10;
    private LevelDataSO _levelData;
    private Transform _playerTransform;
    private PlayerSizeController _playerSizeController;

    public PlayerSizeController Player => _playerSizeController;

    private void OnDisable()
    {
        _playerSizeController.HoleGrown -= TrySpawnBomb;
    }

    public IEnumerator BuildingLevel(LevelDataSO levelData)
    {
        _levelData = levelData;
        PlayerMover player = _spawner.SpawnPlayer(_gameSettings.SpawnPositionPlayer, transform);
        _playerTransform = player.transform;

        _playerSizeController = player.GetComponent<PlayerSizeController>();
        _playerSizeController.HoleGrown += TrySpawnBomb;

        _camera.Follow = _playerTransform;
        _camera.LookAt = _playerTransform;
        int colorCount = levelData.CubeTypes.Count;
        int[] denoms = _gameSettings.Denomiantions;
        int minSmallPerLevel = Mathf.Max(1, _gameSettings.MinCubesPerDenomination);
        int minPerColor = Mathf.CeilToInt((float)minSmallPerLevel / colorCount);

        for (int i = 0; i < colorCount; i++)
        {
            var type = levelData.CubeTypes[i];
            List<int> plan = BuildSpawnPlan(type.CountCubes, minPerColor);

            for (int j = 0; j < plan.Count; j++)
            {
                yield return null;
                Vector3 spawnPosition = GetPositionSpawn();

                Cube cube = _spawner.SpawnCube(spawnPosition, _parentCubes);
                cube.SetColor(type.CurrentColor);
                cube.SetAmount(plan[j]);
                cube.transform.localScale = GetScaleForAmount(plan[j]);

                _spawnPositions.Add(spawnPosition);
                _spawnedCubes.Add(cube);
            }
        }
    }

    private void TrySpawnBomb(int levelHole)
    {
        if (levelHole < 1 && _levelData == null)
            return;

        for (int i = 0; i < _levelData.TotalBombs; i++)
        {
            Vector3 spawnPosition = GetPositionSpawn();
            Vector3 groundPoint = new Vector3(spawnPosition.x, _bombSpawnHeight, spawnPosition.z);
            Bomb bomb = _spawner.SpawnBomb(groundPoint, _parentCubes);

            _spawnedBombs.Add(bomb);
            _spawnPositions.Add(spawnPosition);
        }

        _playerSizeController.HoleGrown -= TrySpawnBomb;
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
        Bounds b = _groundCollider.bounds;
        b.Expand(-_spawnOffsetFromEdge);

        float randomX = UnityEngine.Random.Range(b.min.x, b.max.x);
        float randomZ = UnityEngine.Random.Range(b.min.z, b.max.z);

        for (int i = 0; i < 50; i++)
        {
            Vector3 candidate = new Vector3(randomX, b.center.y, randomZ);
            Vector3 closest = _groundCollider.ClosestPoint(candidate);

            if ((closest - candidate).sqrMagnitude < 0.001f)
                return new Vector3(candidate.x, _spawnYPosition, candidate.z);
        }

        return new Vector3(b.center.x, _spawnYPosition, b.center.z);

        //Vector3 groundSize = _groundBounds.size;
        //float randomX = UnityEngine.Random.Range(
        //    _groundBounds.min.x + _spawnOffsetFromEdge,
        //    _groundBounds.max.x - _spawnOffsetFromEdge);
        //float randomZ = UnityEngine.Random.Range(
        //    _groundBounds.min.z + _spawnOffsetFromEdge,
        //    _groundBounds.max.z - _spawnOffsetFromEdge);

        //return new Vector3(randomX, _spawnYPosition, randomZ);
    }

    private bool IsPositionValid(Vector3 position)
    {
        if (_playerTransform != null &&
    Vector3.Distance(new Vector3(position.x, 0f, position.z), 
    new Vector3(_playerTransform.position.x, 0f, _playerTransform.position.z)) < _gameSettings.MinDistanceFromPlayer)
            return false;

        foreach (Vector3 existingPos in _spawnPositions)
        {
            float distance = Vector3.Distance(position, existingPos);

            if (distance < _gameSettings.MinDistanceBetweenCubes)
                return false;
        }

        return true;
    }

    private List<int> BuildSpawnPlan(int total, int minSmall)
    {
        List<int> result = new List<int>();
        int[] denoms = _gameSettings.Denomiantions;
        minSmall = Mathf.Max(1, minSmall);

        int reserved = 0;
        for (int i = 1; i < denoms.Length; i++)
            reserved += minSmall * denoms[i];

        if (reserved > total)
        {
            minSmall = 1;
            reserved = 0;
            for (int i = 1; i < denoms.Length; i++)
                reserved += denoms[i];
        }

        if (reserved > total)
        {
            while (total > 0) { result.Add(1); total--; }
            return result;
        }

        int remaining = total - reserved;

        if (denoms.Length > 0 && denoms[0] > 0)
        {
            int bigCount = remaining / denoms[0];
            for (int k = 0; k < bigCount; k++) result.Add(denoms[0]);
            remaining -= bigCount * denoms[0];
        }

        for (int i = 1; i < denoms.Length; i++)
        {
            int denom = denoms[i];
            if (denom <= 0) continue;

            for (int k = 0; k < minSmall; k++) result.Add(denom);

            int maxExtra = remaining / denom;
            bool isLast = (i == denoms.Length - 1);

            int extra;
            if (isLast)
            {
                extra = maxExtra;
            }
            else
            {
                if (maxExtra <= 0) continue;
                int minExtra = Mathf.Max(1, Mathf.RoundToInt(maxExtra * _gameSettings.MinFriction));
                int maxExtraClamped = Mathf.Max(minExtra, Mathf.RoundToInt(maxExtra * _gameSettings.MaxFriction));
                extra = UnityEngine.Random.Range(minExtra, maxExtraClamped + 1);
            }

            for (int k = 0; k < extra; k++) result.Add(denom);
            remaining -= extra * denom;
        }

        while (remaining > 0) { result.Add(1); remaining--; }

        return result;
    }

    private Vector3 GetScaleForAmount(int amount)
    {
        float scale = _gameSettings.BaseScale + (amount - 1) * _gameSettings.ScalePerUnit;
        return new Vector3(scale, scale, scale);
    }
}
