using TMPro;
using UnityEngine;

public class CubeView : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private TMP_Text _amountText;

    private void OnEnable()
    {
        _cube.AmmountChanged += OnChangeAmountText;
    }

    private void OnDisable()
    {
        _cube.AmmountChanged -= OnChangeAmountText;
    }

    public void OnChangeAmountText(int amount)
    {
        _amountText.text = amount.ToString();
    }
}
