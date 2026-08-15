using System;
using UnityEngine;

public class FallDetector : MonoBehaviour
{
    public event Action<Cube> CollectCube;
    public event Action<string, LoseReason> GameLose;

    private string LoseDescriptionRU = "Вы подобрали бомбу и проиграли. Будьте осторожнее в следующий раз!";
    private string LoseDescriptionEN = "You picked up a bomb and lost. Be more careful next time!";
    private string LoseDescriptionTR = "Bir bombayı aldınız ve kaybettiniz. Bir dahaki sefere daha dikkatli olun!";

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Cube>(out Cube cube))
            CollectCube?.Invoke(cube);

        if(other.TryGetComponent<Bomb>(out Bomb bomb))
            GameLose?.Invoke(Utils.GetTranslateText(LoseDescriptionRU, LoseDescriptionTR, LoseDescriptionEN), LoseReason.CollectBomb);
    }
}
