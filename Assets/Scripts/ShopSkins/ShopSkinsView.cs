using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopSkinsView : MonoBehaviour
{
    [Inject] private AudioController _audio;
    [SerializeField] private ShopSkins _shopSkins;
    [SerializeField] private GameObject _window;
    [SerializeField] private Button _openWindowButton;
    [SerializeField] private Button _closeWindowButton;
    [SerializeField] private Button _selectButton;
    [SerializeField] private TMP_Text _statusText;

    [SerializeField] private Color _availableButtonColor;
    [SerializeField] private Color _unavailableButtonColor;

    [Header("Translate")]
    [SerializeField] private string _statusSelectRU;
    [SerializeField] private string _statusSelectTR;
    [SerializeField] private string _statusSelectEN;
    [SerializeField] private string _statusSelectedRU;
    [SerializeField] private string _statusSelectedTR;
    [SerializeField] private string _statusSelectedEN;

    private void OnEnable()
    {
        Hide();

        _openWindowButton.onClick.AddListener(Show);
        _closeWindowButton.onClick.AddListener(Hide);
        _selectButton.onClick.AddListener(_shopSkins.UseSkin);
        _shopSkins.UpdateButton += OnUpdatedButton;
        
    }

    private void OnDisable()
    {
        _openWindowButton.onClick.RemoveListener(Show);
        _closeWindowButton.onClick.RemoveListener(Hide);
        _selectButton.onClick.RemoveListener(_shopSkins.UseSkin);
        _shopSkins.UpdateButton -= OnUpdatedButton;
    }

    private void OnUpdatedButton(bool isSelect, bool isUnlocked)
    {
        if (!isSelect && isUnlocked)
        {
            _statusText.text = Utils.GetTranslateText(_statusSelectRU, _statusSelectTR, _statusSelectEN);
            _selectButton.image.color = _availableButtonColor;
            _selectButton.interactable = true;
        }
        else
        {
            _selectButton.image.color = _unavailableButtonColor;
            _selectButton.interactable = false;

            _statusText.text = isUnlocked 
                ? _statusText.text = Utils.GetTranslateText(_statusSelectedRU, _statusSelectedTR, _statusSelectedEN)
                : _statusText.text = Utils.GetTranslateText(_statusSelectRU, _statusSelectTR, _statusSelectEN);                
        }
    }

    private void Show()
    {
        _window.SetActive(true);
        _shopSkins.UpdateInfo();
        _audio.PlayClickSound();
    }

    private void Hide()
    {
        _window.SetActive(false);
        _audio.PlayClickSound();
    }
}
