using UnityEngine;
using Zenject;

public class PlayerCollisionDetector : MonoBehaviour
{
    [Inject] private FallHandler _fallHandler;
    [SerializeField] private Transform _fallPoint;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent<Cube>(out Cube cube))
        {
            collision.collider.isTrigger = true;
            _fallHandler.FallToPoint(cube.transform, _fallPoint);
        }

        if(collision.collider.TryGetComponent<Bomb>(out Bomb bomb))
        {
            collision.collider.isTrigger = true;
            _fallHandler.FallToPoint(bomb.transform, _fallPoint);
        }
    }
}
