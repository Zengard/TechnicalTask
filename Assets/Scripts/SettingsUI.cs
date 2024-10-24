using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Slider _uiMusicSlider;

    private void Awake()
    {
        _uiMusicSlider.value = MusicSettings.MusicVolume;
        _uiMusicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
    }

    private void Update()
    {
        AudioListener.volume = _uiMusicSlider.value;
    }

    private void OnMusicSliderChanged(float value)
    {
        MusicSettings.MusicVolume = value;
    }
}
