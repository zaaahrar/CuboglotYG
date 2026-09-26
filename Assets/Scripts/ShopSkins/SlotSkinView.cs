using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotSkinView : MonoBehaviour
{
    [SerializeField] private SlotSkin _slot;
    [SerializeField] private GameObject _adsWindow;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _adsButton;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private TMP_Text _statusText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _nameText;

    [SerializeField] private Color _availableButtonColor;
    [SerializeField] private Color _unavailableButtonColor;
    [SerializeField] private Color _defaultButtonColor;

    private void OnEnable()
    {
        SlotSkinData skinData = _slot.SkinData;

        if (_slot.SkinData.IsDefault)
        {
            _statusText.text = Utils.GetTranslateText(skinData.StatusRecivedRU, skinData.StatusRecivedTR, skinData.StatusRecivedEN);
            _buyButton.image.color = _defaultButtonColor;
            _buyButton.interactable = false;
            _nameText.text = Utils.GetTranslateText(skinData.NameRU, skinData.NameTR, skinData.NameEN);
            _descriptionText.text = Utils.GetTranslateText(skinData.DescriptionRU, skinData.DescriptionTR, skinData.DescriptionEN);
            return;
        }

        if (_slot.SkinData.AdsEnabled)
            _adsButton.onClick.AddListener(_slot.Shop.ShowAD);

        _slot.InfoUpdated += OnUpdateInfo;
        _buyButton.onClick.AddListener(_slot.TryBuySkin);
    }

    private void OnDisable()
    {
        if (_slot.SkinData.IsDefault)
            return;

        if (_slot.SkinData.AdsEnabled)
            _adsButton.onClick.RemoveListener(_slot.Shop.ShowAD);

        _slot.InfoUpdated -= OnUpdateInfo;
        _buyButton.onClick.RemoveListener(_slot.TryBuySkin);
    }

    private void OnUpdateInfo(int unlockAmount, int price, bool isUnlocked)
    {
        SlotSkinData skinData = _slot.SkinData;

        unlockAmount = Mathf.Min(unlockAmount, price);
        _buyButton.image.color = unlockAmount >= price
            ? _availableButtonColor
            : _unavailableButtonColor;

        _priceText.text = unlockAmount.ToString() + "/" + price.ToString();
        _slider.maxValue = price;
        _descriptionText.text = Utils.GetTranslateText(skinData.DescriptionRU, skinData.DescriptionTR, skinData.DescriptionEN);
        _nameText.text = Utils.GetTranslateText(skinData.NameRU, skinData.NameTR, skinData.NameEN);

        if (isUnlocked)
        {
            _buyButton.image.color = _defaultButtonColor;
            _buyButton.interactable = false;
            _statusText.text = Utils.GetTranslateText(skinData.StatusRecivedRU, skinData.StatusRecivedTR, skinData.StatusRecivedEN);
        }
        else
        {
            _statusText.text = Utils.GetTranslateText(skinData.StatusNotRecivedRU, skinData.StatusNotRecivedTR, skinData.StatusNotRecivedEN);
        }     
 
        _slider.value = unlockAmount;

        if (unlockAmount == price && _slot.SkinData.AdsEnabled)
            _adsWindow.SetActive(false);
    }
}
