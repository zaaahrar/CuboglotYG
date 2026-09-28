using UnityEngine;
using Zenject;

public class PlayerCollisionDetector : MonoBehaviour
{
    [Inject] private FallHandler _fallHandler;
    [SerializeField] private PlayerSizeController _sizeController;
    [SerializeField] private Transform _fallPoint;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent<Cube>(out Cube cube))
        {
            if (cube.Ammount <= _sizeController.Capacity)
            {
                collision.collider.isTrigger = true;
                _fallHandler.FallToPoint(cube.transform, _fallPoint.gameObject);
                _sizeController.AddCubes(cube.Ammount);
            }
        }

        if(collision.collider.TryGetComponent<Bomb>(out Bomb bomb))
        {
            collision.collider.isTrigger = true;
            _fallHandler.FallToPoint(bomb.transform, _fallPoint.gameObject);
        }
    }
}
