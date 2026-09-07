using System;
using UnityEngine;

public class ErrorWindowController : MonoBehaviour
{
    public event Action<string> ShowErrorWindow;

    public void ShowError(string description) => ShowErrorWindow?.Invoke(description);
}
