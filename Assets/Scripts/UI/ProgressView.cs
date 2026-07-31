using UnityEngine.UI;
using TMPro;
using UnityEngine;
using Zenject;

public class ProgressView : MonoBehaviour
{
    [Inject] private CubeCollector _cubeCounter;
    [SerializeField] private TMP_Text _progressText;
    [SerializeField] private Slider _progressSlider;
    
    public void Initialize(int maxCubes)
    {
        _progressSlider.value = 0;
        _progressSlider.maxValue = maxCubes;
        _cubeCounter.ProgressUpdated += OnUpdateCounter;
    }

    private void OnDisable() => _cubeCounter.ProgressUpdated -= OnUpdateCounter;

    public void OnUpdateCounter(int count, int maxCubes)
    {
        _progressSlider.value = count;
        int progress = Mathf.RoundToInt((float)count / maxCubes * 100f);
        _progressText.text = $"{progress}%";
    }
}
