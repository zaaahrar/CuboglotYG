using UnityEngine;
using Zenject;

public class TutorialView : MonoBehaviour
{
    [SerializeField] private GameObject _window;
    [SerializeField] private Tutorial _tutorial;
    [Inject] private AudioController _audio;

    private void OnEnable()
    {
        _tutorial.OpenWindow += OnShowWindow;
    }

    private void OnDisable()
    {
        _tutorial.OpenWindow -= OnShowWindow;
    }

    private void OnShowWindow()
    {
        _window.gameObject.SetActive(true);
        _audio.PlayClickSound();
    }
}
