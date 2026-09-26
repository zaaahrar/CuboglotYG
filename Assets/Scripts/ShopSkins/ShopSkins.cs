using DanielLochner.Assets.SimpleScrollSnap;
using System;
using System.Collections.Generic;
using UnityEngine;
using YG;
using Zenject;

public class ShopSkins : MonoBehaviour
{
    [Inject] private GameSettingsSO _gameSettings;
    [SerializeField] private List<SlotSkin> _slots;
    [SerializeField] private SimpleScrollSnap _scrollSnap;
    [SerializeField] private ErrorWindowController _errorController;

    [SerializeField, TextArea] private string _descriptionErrorRU = "Неудачное воспроизведение рекламы";
    [SerializeField, TextArea] private string _descriptionErrorEN = "Unsuccessful advertisement playback";
    [SerializeField, TextArea] private string _descriptionErrorTR = "Reklamların oynatılamaması";

    public event Action<bool, bool> UpdateButton;
    public int CentredSkin => _scrollSnap.CenteredPanel;

    private void Start()
    {
        _scrollSnap.OnPanelCentered.AddListener(OnPanelChanged);
        YandexGame.RewardVideoEvent += Rewarded;
        YandexGame.ErrorVideoEvent += OnErrorVideo;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
        YandexGame.ErrorVideoEvent -= OnErrorVideo;
    }

    public void UpdateInfo()
    {
        foreach (SlotSkin slot in _slots)
            slot.Refresh();

        UpdateSelectButton(_scrollSnap.CenteredPanel);
        _scrollSnap.GoToPanel(YandexGame.savesData.CurrentSkinIndex);
    }

    public void ShowAD()
    {
        AD ads = new(_gameSettings);
        ads.ShowRewAd(AdPlacementIds.AdsWatchedForSkins);
    }

    public void UseSkin()
    {
        foreach (SlotSkin slot in _slots)
            slot.Unequip();

        _slots[CentredSkin].Equip();

        foreach (SlotSkin slot in _slots)
            slot.Refresh();

        UpdateSelectButton(_scrollSnap.CenteredPanel);
    }

    public void UpdateSelectButton(int indexPanel)
    {
        SlotSkin slot = _slots[indexPanel];

        UpdateButton?.Invoke(slot.IsSelected, slot.IsUnlocked);
    }

    public void Rewarded(int index)
    {
        if (index == AdPlacementIds.AdsWatchedForSkins)
        {
            YandexGame.savesData.TotalAdsWatched++;
            YandexGame.SaveProgress();

            foreach (SlotSkin slot in _slots)
                slot.Refresh();
        }
    }

    private void OnErrorVideo() => _errorController.ShowError(Utils.GetTranslateText(_descriptionErrorRU,
_descriptionErrorTR, _descriptionErrorEN));

    private void OnPanelChanged(int newIndex, int oldIndex)
    {
        UpdateSelectButton(newIndex);
    }
}

public enum SkinUnlockType
{
    Ad,
    Coins,
    Progress
}
