using UnityEngine;

public class MusicUpdater : MonoBehaviour
{
    [SerializeField] private SoundManager _soundManager;
    private AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _soundManager.OnVolumeChanged += OnSoundUpdate;
        OnSoundUpdate();
    }

    private void OnSoundUpdate()
    {
        _audioSource.volume = _soundManager.GetMusicVolume();
    }
}
