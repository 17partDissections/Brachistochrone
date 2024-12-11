using UnityEngine;
using UnityEngine.Audio;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _SFX;


    private void Awake()
    {
        _audioMixerGroup.audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        _audioMixerGroup.audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
    }
    private void OnDestroy()
    {
        _audioMixerGroup.audioMixer.GetFloat("MusicVolume", out float mValue);
        PlayerPrefs.SetFloat("MusicVolume", mValue);
        _audioMixerGroup.audioMixer.GetFloat("SFXVolume", out float sfxValue);
        PlayerPrefs.SetFloat("SFXVolume", sfxValue);
    }

    public void PlaySFX(AudioClip audioClip, float volume)
    {
        _SFX.volume = volume;
        _SFX.PlayOneShot(audioClip);
    }
    public void PlayMusic(AudioClip audioClip, float volume)
    {
        _SFX.volume = volume;
        _music.PlayOneShot(audioClip);
    }
    public void StopMusic()
    {
        _music.Stop();
    }
    public void OnMasterVolumeValueChanged(float percent)
    {
        _audioMixerGroup.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, percent));
    }
    public void OnMusicVolumeValueChanged(float percent)
    {
        _audioMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, percent));
    }
    public void OnSFXVolumeValueChanged(float percent)
    {
        _audioMixerGroup.audioMixer.SetFloat("SFXVolume", Mathf.Lerp(-80, 0, percent));
        
    }
    public void OnMusicVolumeValueChangedBySlider(UnityEngine.UI.Slider slider)
    {
        var percent = slider.value;
        _audioMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, percent));
            PlayerPrefs.SetFloat("MusicSlider", percent);
    }
    public void OnSFXVolumeValueChangedBySlider(UnityEngine.UI.Slider slider)
    {
        var percent = slider.value;
        _audioMixerGroup.audioMixer.SetFloat("SFXVolume", Mathf.Lerp(-80, 0, percent));
            PlayerPrefs.SetFloat("SFXSlider", percent);

    }

}

