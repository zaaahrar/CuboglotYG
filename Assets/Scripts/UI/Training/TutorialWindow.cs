using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TutorialWindow : MonoBehaviour
{
    [SerializeField] private GameObject _nextWindow;
    [SerializeField] private Button _button;
    [SerializeField] private bool _isLastWindow = false;
    [SerializeField] private Tutorial _tutorial;
    [Inject] private AudioController _audio;

    private void OnEnable()
    {
        _button.onClick.AddListener(ShowNextWindow);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ShowNextWindow);
    }

    private void ShowNextWindow()
    {
        if (!_isLastWindow)
        {
            _nextWindow.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            _tutorial.EndTraining();
            gameObject.SetActive(false);
        }

        _audio.PlayClickSound();
    }
}
