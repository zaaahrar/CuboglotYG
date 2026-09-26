using UnityEngine;
using Zenject;

public class Cube : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Rigidbody _rigidBody;
    [Inject] private GameSettingsSO _gameSettings;
    [Inject] private MaterialParser _materialParser;

    public ColorCube CurrentColor { get; private set; }

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
