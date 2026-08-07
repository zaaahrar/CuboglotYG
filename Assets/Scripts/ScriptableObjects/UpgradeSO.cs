using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Game/Upgrade")]
public class UpgradeSO : ScriptableObject
{
    public string Name;
    public Upgrades Upgrade;
    public int MaxLevel;
    public int[] Prices;
    public float StatValue;
    public string UpgradeLevelKey;
    public Sprite Sprite;

    private void OnValidate()
    {
        switch (Upgrade)
        {
            case Upgrades.Time:
                UpgradeLevelKey = SaveDataKeys.TimeUpgradeLevel;
                break;
            case Upgrades.Size:
                UpgradeLevelKey = SaveDataKeys.SizeUpgradeLevel;
                break;
            case Upgrades.Speed:
                UpgradeLevelKey = SaveDataKeys.SpeedUpgradeLevel;
                break;
        }
    }
}

public enum Upgrades
{
    Time,
    Size,
    Speed
}
