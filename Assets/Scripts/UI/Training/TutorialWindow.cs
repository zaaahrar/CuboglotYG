using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Zenject;

public class TutorialWindow : MonoBehaviour
{
    [SerializeField] private GameObject _nextWindow;
    [SerializeField] private Button _button;
    [SerializeField] private bool _isLastWindow = false;
    [SerializeField] private Tutorial _tutorial;
    [Inject] private AudioController _audio;

    [Header("Change text for the phone")]
    [SerializeField] private bool _useMobileText;
    [SerializeField] private TMP_Text[] _texts;
    [SerializeField, TextArea] private string[] _mobileTextsRU;
    [SerializeField, TextArea] private string[] _mobileTextsEN;
    [SerializeField, TextArea] private string[] _mobileTextsTR;

    private void OnEnable()
    {
        _button.onClick.AddListener(ShowNextWindow);

        if (_useMobileText && YandexGame.EnvironmentData.isMobile && _texts.Length > 0)
        {
            for (int i = 0; i < _texts.Length; i++)
                _texts[i].text = Utils.GetTranslateText(_mobileTextsRU[i], _mobileTextsTR[i], _mobileTextsEN[i]);
        }
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
