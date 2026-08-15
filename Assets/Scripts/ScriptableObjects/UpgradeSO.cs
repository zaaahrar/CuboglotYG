using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Game/Upgrade")]
public class UpgradeSO : ScriptableObject
{
    public string NameRU;
    public string NameEN;
    public string NameTR;
    public Upgrades Upgrade;
    public int MaxLevel;
    public int[] Prices;
    public float StatValue;
    public Sprite Sprite;
}

public enum Upgrades
{
    Time,
    Size,
    Speed
}
