using System;
using UnityEngine;

public class FallObjectsTriggerCheker : MonoBehaviour
{
    public event Action<Cube> CollectCube;
    public event Action CollectBomb;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Cube>(out Cube cube))
        {
            CollectCube?.Invoke(cube);
            Destroy(cube.gameObject);
        }

        if(other.TryGetComponent<Bomb>(out Bomb bomb))
        {
            CollectBomb?.Invoke();
        }
    }
}
