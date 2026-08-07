using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Zenject;

public class Upgrade : MonoBehaviour
{
    [Inject] private GoldHandler _goldHandler;
    [Inject] private AudioController _audio;

    [SerializeField] private UpgradeSO _upgrade;
    [SerializeField] private Image _goldImage;
    [SerializeField] private Image _upgradeImage;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Button _buyButton;

    [SerializeField] private GameObject _levelInstance;
    [SerializeField] private Transform _levelParent;

    private int _currentLevel = 0;
    private List<GameObject> _levelImages = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < _upgrade.MaxLevel; i++)
            _levelImages.Add(Instantiate(_levelInstance, _levelParent));

        if (PlayerPrefs.HasKey(_upgrade.UpgradeLevelKey))
        {
            _currentLevel = PlayerPrefs.GetInt(_upgrade.UpgradeLevelKey);
        }
        else
        {
            _currentLevel = 0;
            PlayerPrefs.SetInt(_upgrade.UpgradeLevelKey, _currentLevel);
        }


        if(_currentLevel > 0)
        {
            for (int i = 0; i < _currentLevel; i++)
                _levelImages[i].GetComponent<Image>().color = Color.green;
        }

        UpdateInfoPrice();
        _nameText.text = _upgrade.Name;
        _upgradeImage.sprite = _upgrade.Sprite;
    }

    private void OnEnable()
    {
        UpdateInfoPrice();
        _buyButton.onClick.AddListener(Buy);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(Buy);
    }

    public void Buy()
    {
        if (_goldHandler.TrySpendGold(_upgrade.Prices[_currentLevel]))
        {
            Debug.Log("S");
            _audio.PlayGoldSound();
            _goldHandler.SpendGold(_upgrade.Prices[_currentLevel]);
            _currentLevel++;
            PlayerPrefs.SetInt(_upgrade.UpgradeLevelKey, _currentLevel);
            UpdateInfoPrice();
            _levelImages[_currentLevel - 1].GetComponent<Image>().color = Color.green;
        }
    }

    private void UpdateInfoPrice()
    {
        if (_currentLevel >= _upgrade.MaxLevel)
        {
            _buyButton.interactable = false;
            _buyButton.gameObject.GetComponent<Image>().color = Color.gray;
            _goldImage.gameObject.SetActive(false);
            _priceText.text = "Максимум";
        }
        else
        {
            _priceText.text = _upgrade.Prices[_currentLevel].ToString();
        }
    }
}
