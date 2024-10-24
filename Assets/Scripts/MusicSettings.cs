using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public static class MusicSettings
{
    public static UnityEvent<float> OnMusicVolumeChanged;
    private static float _musicVolume;

    public static float MusicVolume
    {
        get { return _musicVolume; }
        set
        {
            _musicVolume = value;
            PlayerPrefs.SetFloat("MusicVolume", value);
            if (OnMusicVolumeChanged != null)
            {
                OnMusicVolumeChanged.Invoke(value);
            }
        }
    }
    static MusicSettings()
    {
        _musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
    }
}
