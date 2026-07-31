using System;
using UnityEngine;

public class FallDetector : MonoBehaviour
{
    public event Action<Cube> CollectCube;
    public event Action GameLose;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Cube>(out Cube cube))
            CollectCube?.Invoke(cube);

        if(other.TryGetComponent<Bomb>(out Bomb bomb))
            GameLose?.Invoke();
    }
}
