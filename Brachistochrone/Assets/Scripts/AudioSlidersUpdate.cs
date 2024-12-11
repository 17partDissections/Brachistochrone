using System;
using UnityEngine;

public class AudioSlidersUpdate : MonoBehaviour
{
    private enum SliderType { MusicSlider, SFXSlider }
    [SerializeField] private SliderType _sliderType;
    private void Awake()
    {
        TryGetComponent<UnityEngine.UI.Slider>(out UnityEngine.UI.Slider slider);
        var value = PlayerPrefs.GetFloat(_sliderType.ToString());
        slider.value = value;
    }
}
