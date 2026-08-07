using TMPro;
using UnityEngine;
using Zenject;

public class GoldView : MonoBehaviour
{
    [Inject] private GoldHandler _goldHandler;
    [SerializeField] private TMP_Text _goldText;

    private void OnEnable()
    {
        _goldHandler.UpdateGold += OnUpdateGold;
        OnUpdateGold(_goldHandler.CurrentGold);
    }

    private void OnDisable() => _goldHandler.UpdateGold -= OnUpdateGold;

    private void OnUpdateGold(int gold) => _goldText.text = gold.ToString();
}
