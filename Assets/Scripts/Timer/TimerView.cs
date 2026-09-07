using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class TimerView : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _timerText;

    private void OnDisable()
    {
        _timer.UpdateTimer -= OnUpdateTimer;
    }

    public void Initialize(LevelDataSO levelData)
    {
        _timer.UpdateTimer += OnUpdateTimer;
        _slider.maxValue = levelData.TimeLimit 
            + (int)YandexGame.savesData.LevelTimeUpgrade * _timer.TimeUpgrade.StatValue;
        _slider.value = _slider.maxValue;
        OnUpdateTimer(levelData.TimeLimit);
    }

    private void OnUpdateTimer(int timerDureation)
    {
        int minutes = timerDureation / 60;
        int seconds = timerDureation % 60;

        if(seconds < 10)
            _timerText.text = $"{minutes}:0{seconds}";
        else
            _timerText.text = $"{minutes}:{seconds}";

        _slider.value = timerDureation;
    }
}
