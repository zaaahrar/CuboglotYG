using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenView : MonoBehaviour
{
    private const int MaxValueSlider = 100;

    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _loadingSlider;

    public void Initialize()
    {
        _loadingSlider.value = 0;
        _loadingSlider.maxValue = MaxValueSlider;
    }

    public void Show()
    {
        _loadingScreen.SetActive(true);
    }

    public void Hide()
    {
        _loadingScreen.SetActive(false);
    }

    public void ChangeSlider(int value)
    {
        _loadingSlider.value = value;
    }
}
