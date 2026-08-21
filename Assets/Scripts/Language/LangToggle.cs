using UnityEngine;
using UnityEngine.UI;


[RequireComponent (typeof(Toggle))]
public class LangToggle : MonoBehaviour
{
    [SerializeField] private string _languageName;
    [SerializeField] private Toggle _toggle;

    public string LanguageName => _languageName;
    public bool IsOn => _toggle.isOn;

    public void OnToggle() => _toggle.isOn = true;
}
