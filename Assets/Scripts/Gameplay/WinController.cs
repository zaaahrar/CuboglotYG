using System;
using UnityEngine;

public class WinController : MonoBehaviour
{
    public event Action ShowWinScreen;
    public event Action<int, int> UpdateWinScreen;

    public void Win(int cubesCollect, int gold)
    {
        UpdateWinScreen?.Invoke(cubesCollect, gold);
        ShowWinScreen?.Invoke();
    }
}
