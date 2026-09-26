using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UpgradeScreenView : MonoBehaviour
{
    [Inject] AudioController _audio;

    [SerializeField] private Upgrade[] _upgrades;
    [SerializeField] private GameObject _window;
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Image _notification;

    private void Start()
    {
        _openButton.onClick.AddListener(Show);
        _closeButton.onClick.AddListener(Hide);

        foreach (Upgrade upgrade in _upgrades)
        {
            upgrade.PurchaseCompleted += UpdateColorButtonsUpgrades;
            upgrade.Initialize();
        }


        _window.SetActive(false);
        SetNotification();
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(Show);
        _closeButton.onClick.RemoveListener(Hide);

        foreach (Upgrade upgrades in _upgrades)
            upgrades.PurchaseCompleted -= UpdateColorButtonsUpgrades;
    }

    public void Show()
    {
        _window.SetActive(true);
        _audio.PlayClickSound();
    }

    public void Hide()
    {
        _window.SetActive(false);
        _audio.PlayClickSound();
        SetNotification();
    }

    public void SetNotification()
    {
        _notification.gameObject.SetActive(FindAffordableUpgrade());
    }

    public bool FindAffordableUpgrade()
    {
        foreach (var upgrade in _upgrades)
        {
            if (upgrade.CanBuy())
                return true;
        }

        return false;
    }

    public void UpdateColorButtonsUpgrades()
    {
        foreach (var upgrade in _upgrades)
            upgrade.UpdateColorButton();
    }
}
