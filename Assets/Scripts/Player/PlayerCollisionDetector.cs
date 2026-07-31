using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    [SerializeField] private Transform _fallPoint;
    [SerializeField] private HoleDropHandler _dropHandler;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent<Cube>(out Cube cube))
        {
            collision.collider.isTrigger = true;
            _dropHandler.FallToPoint(cube.transform, _fallPoint);
        }

        if(collision.collider.TryGetComponent<Bomb>(out Bomb bomb))
        {
            collision.collider.isTrigger = true;
            _dropHandler.FallToPoint(bomb.transform, _fallPoint);
        }
    }
}
