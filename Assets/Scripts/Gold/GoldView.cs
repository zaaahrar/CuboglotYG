using TMPro;
using UnityEngine;

public class GoldView : MonoBehaviour
{
    [SerializeField] private GoldHandler _goldHandler;
    [SerializeField] private TMP_Text _goldText;

    public void Initialize() => _goldHandler.UpdateGold += OnUpdateGold;

    private void OnDisable() => _goldHandler.UpdateGold -= OnUpdateGold;

    private void OnUpdateGold(int gold) => _goldText.text = gold.ToString();
}
