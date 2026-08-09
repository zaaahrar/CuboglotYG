
using UnityEngine;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public int Gold = 0;
        public int CurrentLevelIndex = 0;

        [Header("Upgrades")]
        public int LevelSpeedUpgrade = 0;
        public int LevelSizeUpgrade = 0;
        public int LevelTimeUpgrade = 0;

        [Header("Settings")]
        public int VolumeGame = 50;
        public bool IsSoundOn = true;

        public SavesYG()
        {

        }
    }
}
