using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;

    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private PlayerMover _playerPrefab;

    public Cube SpawnCube(Vector3 position, Transform parent)
    {
        var cubeObject = _diContainer.InstantiatePrefab(_cubePrefab, position, Quaternion.identity, parent);
        Cube cube = cubeObject.GetComponent<Cube>();
        return cube;
    }

    public Bomb SpawnBomb(Vector3 position, Transform parent)
    {
        Bomb bomb = Instantiate(_bombPrefab, position, _bombPrefab.transform.rotation, parent);
        return bomb;
    }

    public PlayerMover SpawnPlayer(Vector3 position, Transform parent)
    {
        var playerObject = _diContainer.InstantiatePrefab(_playerPrefab, position, Quaternion.identity, parent);
        PlayerMover player = playerObject.GetComponent<PlayerMover>();
        player.Init(_joystick);
        return player;
    }
}
