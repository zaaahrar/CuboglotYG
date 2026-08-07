using UnityEngine;
using System;
using System.Collections;
using Zenject;

public class Timer : MonoBehaviour
{
    [SerializeField] private UpgradeSO _timeUpgrade;
    [Inject] private AudioController _audioController;

    private LevelDataSO _levelData;
    private WaitForSeconds _second;
    private int _timerDuration;
    private int _tickStartTime = 10;

    public event Action<int> UpdateTimer;
    public event Action TimerFinished;

    public UpgradeSO TimeUpgrade => _timeUpgrade;
    public int TimerDuration
    {
        get => _timerDuration;
        private set
        {
            _timerDuration = value;
            UpdateTimer?.Invoke(TimerDuration);
        }
    }

    public void Initialize(LevelDataSO levelData)
    {
        if(levelData == null)
            throw new ArgumentNullException(nameof(levelData));

        _levelData = levelData;
        TimerDuration = _levelData.TimeLimit + (int)(PlayerPrefs.GetInt(_timeUpgrade.UpgradeLevelKey) * _timeUpgrade.StatValue);
        _second = new WaitForSeconds(1);
    }

    public IEnumerator StartTimer()
    {
        while(_timerDuration > 0)
        {
            yield return _second;

            if(TimerDuration <= _tickStartTime)
                _audioController.PlayTickSound();

            TimerDuration--;
        }

        TimerFinished?.Invoke();
    }
}
