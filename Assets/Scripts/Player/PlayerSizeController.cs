using UnityEngine;
using YG;

public class PlayerSizeController : MonoBehaviour
{
    [SerializeField] private UpgradeSO _sizeUpgrade;

    private float _statSize;

    private void Start()
    {
        _statSize = transform.localScale.x + YandexGame.savesData.LevelSizeUpgrade * _sizeUpgrade.StatValue;
        transform.localScale = new Vector3(_statSize, _statSize, _statSize); 
    }
}
