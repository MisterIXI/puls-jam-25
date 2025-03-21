using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider soundSlider;
    [SerializeField] Slider musicSlider;
    public Action OnVolumeChanged;
    float tempMusicVolume;
    float tempSoundVolume;
    void Awake()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            Load();
        }
        else
        {
            SetMusicVolume();
            SetSoundVolume();
        }
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        PlayerPrefs.SetFloat("musicVolume", volume);
        tempMusicVolume = volume;
        OnVolumeChanged?.Invoke();
    }

    public void SetSoundVolume()
    {

        float volume = soundSlider.value;
        PlayerPrefs.SetFloat("soundVolume", volume);
        tempSoundVolume = volume;
    }

    public void Load()
    {
        float savedMusicVolume = PlayerPrefs.GetFloat("musicVolume");
        float savedSoundVolume = PlayerPrefs.GetFloat("soundVolume");


        if (musicSlider != null) musicSlider.value = savedMusicVolume;
        if (soundSlider != null) soundSlider.value = savedSoundVolume;

        // Store the initial values
        tempMusicVolume = musicSlider.value;
        tempSoundVolume = soundSlider.value;



    }

    public void Save()
    {
        SetMusicVolume();
        SetSoundVolume();
    }


    public void ResetSliders()
    {
        musicSlider.value = tempMusicVolume;
        soundSlider.value = tempSoundVolume;
    }

    public float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("musicVolume");
    }

    public float GetSoundVolume()
    {
        return PlayerPrefs.GetFloat("soundVolume");
    }
}
