using UnityEngine;

public class PlayerSizeController : MonoBehaviour
{
    [SerializeField] private UpgradeSO _sizeUpgrade;

    private float _statSize;

    private void Start()
    {
        _statSize = 1 + PlayerPrefs.GetInt(_sizeUpgrade.UpgradeLevelKey) * _sizeUpgrade.StatValue;
        transform.localScale = new Vector3(_statSize, _statSize, _statSize); 
    }
}
