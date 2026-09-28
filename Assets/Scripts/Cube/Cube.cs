using UnityEngine;
using Zenject;
using System;

public class Cube : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Rigidbody _rigidBody;
    [Inject] private GameSettingsSO _gameSettings;
    [Inject] private MaterialParser _materialParser;

    public event Action<int> AmmountChanged;

    private int _amount = 1;

    public ColorCube CurrentColor { get; private set; }
    public int Ammount
    {
        get => _amount;
        private set
        {
            _amount = value;
            AmmountChanged?.Invoke(_amount);
        }
    }

    public void SetAmount(int value) => Ammount = value;

    public void SetColor(ColorCube color)
    {
        CurrentColor = color;
        _renderer.material = _materialParser.GetMaterial(CurrentColor);
    }

    public void SetKinematic(bool isKinematic) => _rigidBody.isKinematic = isKinematic;

    public void Explode()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere.normalized;
        _rigidBody.AddForce(randomDirection * _gameSettings.ExplodeForce, ForceMode.Impulse);
    }
}
