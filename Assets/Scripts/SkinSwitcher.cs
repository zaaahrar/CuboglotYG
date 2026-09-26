using UnityEngine;
using Zenject;
using YG;

public class SkinSwitcher : MonoBehaviour
{
    [Inject] private GameSettingsSO _gameSettings;
    [SerializeField] private MeshRenderer _playerSkin;

    public void SwitchSkin()
    {
        if (YandexGame.savesData.CurrentSkinIndex < _gameSettings.Skins.Length)
            _playerSkin.material = _gameSettings.Skins[YandexGame.savesData.CurrentSkinIndex];
        else
            _playerSkin.material = _gameSettings.Skins[0];
    }
}
