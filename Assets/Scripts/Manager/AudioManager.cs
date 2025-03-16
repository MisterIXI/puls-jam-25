using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private List<AudioSource> _audioSources = new List<AudioSource>();
    [SerializeField] private int _audioSourceCount = 10;
    
    [Header("Background Music")]
    [SerializeField] private AudioClip backgroundMusic;
    private AudioSource _backgroundMusicSource;
    
    [Header("Background Sounds")]
    [SerializeField] private AudioClip backgroundSound;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < _audioSourceCount; i++)
        {
            GameObject audioSource = new GameObject("AudioSource_" + i);
            audioSource.transform.SetParent(transform);
            _audioSources.Add(audioSource.AddComponent<AudioSource>());
            _audioSources[i] = audioSource.AddComponent<AudioSource>();
        }
        GameObject backgroundMusicSourceObject = new GameObject("BackgroundMusicSource");
        backgroundMusicSourceObject.transform.SetParent(transform);
        _backgroundMusicSource = backgroundMusicSourceObject.AddComponent<AudioSource>();
        
        PlayBackgroundMusic(backgroundMusic);
        PlayClip(backgroundSound, Vector3.zero,true, PlayerPrefs.GetFloat("soundVolume"));
    }

    public AudioSource PlayClip(AudioClip clip, Vector3 positionToPlay, bool loop = false, float volume = 0.5f, float pitch = 1f)
    {
        for (int i = 0; i < _audioSourceCount; i++)
        {
            if (!_audioSources[i].isPlaying)
            {
                _audioSources[i].clip = clip;
                _audioSources[i].volume = volume;
                _audioSources[i].pitch = pitch;
                _audioSources[i].transform.position = positionToPlay;
                _audioSources[i].Play();
                _audioSources[i].loop = loop;
                return _audioSources[i];
            }
        }
        AudioSource newAudioSource = AddAudioSource();
        newAudioSource.clip = clip;
        newAudioSource.volume = volume;
        newAudioSource.pitch = pitch;
        newAudioSource.transform.position = positionToPlay;
        newAudioSource.loop = loop;
        newAudioSource.Play();
        return newAudioSource;
    }

    public AudioSource PlayClip(AudioClip clip, Vector3 positionToPlay, float volume = 0.5f, float pitch = 1f)
    {
        return PlayClip(clip, positionToPlay,false, volume, pitch);
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        _backgroundMusicSource.clip = clip;
        _backgroundMusicSource.loop = true;
        _backgroundMusicSource.volume = PlayerPrefs.GetFloat("musicVolume");
        _backgroundMusicSource.Play();
    }
    


    private AudioSource AddAudioSource()
    {
        GameObject audioSource = new GameObject("AudioSource_" + _audioSources.Count);
        audioSource.transform.SetParent(transform);
        AudioSource newAudioSource = audioSource.AddComponent<AudioSource>();
        _audioSources.Add(newAudioSource);
        return newAudioSource;
    }
}
