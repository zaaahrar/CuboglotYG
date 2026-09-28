using System;
using UnityEngine;
using YG;
using Zenject;

public class SlotSkin : MonoBehaviour
{
    [Inject] private AudioController _audio;
    [Inject] private GoldHandler _goldHandler;
    [SerializeField] private SlotSkinData _slotSkinData;
    [SerializeField] private ShopSkins _shop;

    public event Action<int, int, bool> InfoUpdated;

    private bool _isUnlocked;
    private bool _isSelected;

    public SlotSkinData SkinData => _slotSkinData;
    public bool IsSelected => _isSelected;
    public bool IsUnlocked => _isUnlocked;
    public ShopSkins Shop => _shop;

    public void Refresh()
    {
        _isUnlocked = YandexGame.savesData.UnlockedSkins.Contains(_slotSkinData.SkinIndex);
        _isSelected = YandexGame.savesData.CurrentSkinIndex == _slotSkinData.SkinIndex;
        InfoUpdated?.Invoke(GetUnlockAmount(_slotSkinData.UnlockType), _slotSkinData.Price, _isUnlocked);
    }

    public void Equip()
    {
        _isSelected = true;
        _audio.PlayClickSound();
        YandexGame.savesData.CurrentSkinIndex = _slotSkinData.SkinIndex;
        YandexGame.SaveProgress();
    }

    public void Unequip() => _isSelected = false;

    public void TryBuySkin()
    {
        if(GetUnlockAmount(_slotSkinData.UnlockType) >= _slotSkinData.Price)
        {
            InfoUpdated?.Invoke(GetUnlockAmount(_slotSkinData.UnlockType), _slotSkinData.Price, _isUnlocked);
            _isUnlocked = true;

            if (_slotSkinData.UnlockType == SkinUnlockType.Coins)
                _goldHandler.SpendGold(_slotSkinData.Price);

            YandexGame.savesData.UnlockedSkins.Add(_slotSkinData.SkinIndex);
            YandexGame.SaveProgress();
            _shop.UpdateSelectButton(_slotSkinData.SkinIndex);
            _audio.PlaySuccessfulActionSound();
            Refresh();
        }
    }

    private int GetUnlockAmount(SkinUnlockType unlockType)
    {
        switch (unlockType)
        {
            case SkinUnlockType.Ad:
                return YandexGame.savesData.TotalAdsWatched;
            case SkinUnlockType.Coins:
                return YandexGame.savesData.Gold;
            case SkinUnlockType.Progress:
                return YandexGame.savesData.TotalLevelsComleted;
            default: return 0;
        }
    }
}
