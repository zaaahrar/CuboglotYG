using YG;
using UnityEngine;

public class AD
{
    private GameSettingsSO _gameSettingsSO;

    public AD(GameSettingsSO gameSettingsSO)
    {
        _gameSettingsSO = gameSettingsSO;
    }

    public void TryShowFullscreenAd()
    {
        if(YandexGame.savesData.LevelsSinceLastAd >= _gameSettingsSO.LevelsBeforeAd)
        {
            YandexGame.FullscreenShow();
            Debug.Log("AD SHOW");
            YandexGame.savesData.LevelsSinceLastAd = 0;
            YandexGame.SaveProgress();
        }
    }

    public void ShowRewAd(int indexAd) => YandexGame.RewVideoShow(indexAd);
}
