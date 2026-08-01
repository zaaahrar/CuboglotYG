using UnityEngine;
using System;
using System.Collections;

public class Timer : MonoBehaviour
{
    private LevelDataSO _levelData;

    public event Action<int> UpdateTimer;
    public event Action TimerFinished;

    private WaitForSeconds _second;
    private int _timerDuration;

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
        TimerDuration = _levelData.TimeLimit;
        _second = new WaitForSeconds(1);
    }

    public IEnumerator StartTimer()
    {
        while(_timerDuration > 0)
        {
            yield return _second;
            TimerDuration--;
        }

        TimerFinished?.Invoke();
    }
}
