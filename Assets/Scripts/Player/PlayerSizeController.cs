using DG.Tweening;
using UnityEngine;
using Zenject;
using System;

public class PlayerSizeController : MonoBehaviour
{
    [Inject] private GameSettingsSO _gameSettings;

    private int _levelGrowth;
    private int _currentCubes = 0;
    private Vector3 _baseScale = new Vector3(0.7f, 1, 0.7f);

    public event Action<int> HoleGrown;

    public int Capacity { get; private set; }

    private void Start()
    {
        _levelGrowth = 0;
        _currentCubes = 0;
        Capacity =  _gameSettings.CapacitysPlayer[_levelGrowth];
    }

    public void AddCubes(int cubes)
    {
        _currentCubes += cubes;

        if (!CanGrow() || _currentCubes < _gameSettings.CubesToNextGrowth[_levelGrowth])
            return;

        _currentCubes = 0;
        _levelGrowth++;
        Capacity = _gameSettings.CapacitysPlayer[_levelGrowth];
        Vector3 newScale = _baseScale + new Vector3(_gameSettings.GrowthStep * _levelGrowth, 0f, _gameSettings.GrowthStep * _levelGrowth);
        Grow(newScale);
        HoleGrown?.Invoke(Capacity);
    }

    private bool CanGrow() =>
        _levelGrowth < _gameSettings.CubesToNextGrowth.Length &&
        _levelGrowth + 1 < _gameSettings.CapacitysPlayer.Length;

    private void Grow(Vector3 newScale) => transform.DOScale(newScale, _gameSettings.DurationGrow).SetLink(gameObject, LinkBehaviour.KillOnDestroy);
}
