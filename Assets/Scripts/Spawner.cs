using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;

    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Bomb _bombPrefab;

    public Cube SpawnCube(Vector3 position, Transform parent)
    {
        var gameObject = _diContainer.InstantiatePrefab(_cubePrefab, position, Quaternion.identity, parent);
        Cube cube = gameObject.GetComponent<Cube>();
        cube.Initialize();
        return cube;
    }

    public Bomb SpawnBomb(Vector3 position, Transform parent)
    {
        Bomb bomb = Instantiate(_bombPrefab, position, _bombPrefab.transform.rotation, parent);
        return bomb;
    }
}
