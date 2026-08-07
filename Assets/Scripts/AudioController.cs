using System;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private const float LOOP_VOLUME_PERCENT = 0.5f;

    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioSource _loopAudio;

    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _successfulActionSound;
    [SerializeField] private AudioClip _boomSound;
    [SerializeField] private AudioClip _collectSound;
    [SerializeField] private AudioClip _tickSound;
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _goldSound;

    private bool _isSoundOn;
    private float _volume;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(SaveDataKeys.VolumeGame))
            _volume = 100;
        else
            _volume = PlayerPrefs.GetInt(SaveDataKeys.VolumeGame);

        if (!PlayerPrefs.HasKey(SaveDataKeys.IsSoundOn))
            _isSoundOn = true;
        else
            _isSoundOn = Convert.ToBoolean(PlayerPrefs.GetInt(SaveDataKeys.IsSoundOn));

        if (!_isSoundOn)
        {
            _audio.volume = 0;
            _loopAudio.volume = 0;
            return;
        }

        _audio.volume = _volume / 100;
        _loopAudio.volume = (_volume / 100) * LOOP_VOLUME_PERCENT;
    }

    public void PlaySceneThem(AudioClip clip)
    {
        _loopAudio.clip = clip;
        _loopAudio.Play();
    }

    public void PlayGoldSound() => _audio.PlayOneShot(_goldSound);

    public void PlayWinSound() => _audio.PlayOneShot(_winSound);

    public void PlaySuccessfulActionSound() => _audio.PlayOneShot(_successfulActionSound);

    public void PlayTickSound() => _audio.PlayOneShot(_tickSound);

    public void PlayCollectSound() => _audio.PlayOneShot(_collectSound);

    public void PlayClickSound() => _audio.PlayOneShot(_clickSound);

    public void PlayBoomSound() => _audio.PlayOneShot(_boomSound);

    public void DisableAllSounds()
    {
        _audio.Stop();
        _loopAudio.Stop();
    }

    public void UpdateSettings(bool isSoundOn, int volume)
    {
        _volume = volume;
        _isSoundOn = isSoundOn;

        if (!_isSoundOn)
        {
            _audio.volume = 0;
            _loopAudio.volume = 0;
            return;
        }

        _audio.volume = _volume / 100;
        _loopAudio.volume = _volume / 100;
    }
}
