using UnityEngine;
using YG;

public class TabStateHandler : MonoBehaviour
{
    private void Awake() => DontDestroyOnLoad(this.gameObject);

    private void OnEnable() => YandexGame.onVisibilityWindowGame += ChangeStateGame;

    private void OnDisable() => YandexGame.onVisibilityWindowGame -= ChangeStateGame;

    private void ChangeStateGame(bool visible)
    {
        Time.timeScale = visible ? 1 : 0;
        AudioListener.pause = !visible;
    }
}
