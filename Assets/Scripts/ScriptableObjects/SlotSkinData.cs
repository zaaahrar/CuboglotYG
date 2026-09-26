using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/SlotSkinData")]
public class SlotSkinData : ScriptableObject
{
    public SkinUnlockType UnlockType;
    public int Price;
    public int SkinIndex;
    public bool IsDefault;
    public bool AdsEnabled;

    public string NameRU;
    public string NameEN;
    public string NameTR;
    [TextArea]
    public string DescriptionRU;
    [TextArea]
    public string DescriptionEN;
    [TextArea]
    public string DescriptionTR;

    [TextArea] public string StatusRecivedRU;
    [TextArea] public string StatusRecivedEN;
    [TextArea] public string StatusRecivedTR;

    [TextArea] public string StatusNotRecivedRU;
    [TextArea] public string StatusNotRecivedEN;
    [TextArea] public string StatusNotRecivedTR;
}
