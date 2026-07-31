using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private Bomb _bomb;

    public Cube SpawnCube(Vector3 position, Transform parent)
    {
        Cube cube = Instantiate(_cube, position, Quaternion.identity, parent);
        cube.Initialize();
        return cube;
    }

    public Bomb SpawnBomb(Vector3 position, Transform parent)
    {
        Bomb bomb = Instantiate(_bomb, position, _bomb.transform.rotation, parent);
        return bomb;
    }
}
