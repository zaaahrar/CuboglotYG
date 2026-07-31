using UnityEngine;

public class WinScreenView : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;

    public void Show() => _winScreen.SetActive(true);

    public void Hide() => _winScreen.SetActive(false);
}
