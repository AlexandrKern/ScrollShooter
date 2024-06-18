using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour, ISoundManager
{
    public static MusicManager instance;
    private float volume = 1f;
    private bool isMuted = false;
    private AudioSource audioSource;
    private Dictionary<string, AudioClip> musicClips;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.GetComponent<AudioSource>();
            musicClips = new Dictionary<string, AudioClip>();

            volume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
            ApplyVolumeToAudioSource();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetVolume(float newVolume)
    {
        volume = newVolume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
        ApplyVolumeToAudioSource();
    }

    public float GetVolume()
    {
        return volume;
    }

    private void ApplyVolumeToAudioSource()
    {
        audioSource.volume = isMuted ? 0 : volume;
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        ApplyVolumeToAudioSource();
    }

    public bool IsMuted()
    {
        return isMuted;
    }

    public void AddMusicClip(string key, AudioClip clip)
    {
        if (!musicClips.ContainsKey(key))
        {
            musicClips.Add(key, clip);
        }
    }

    public void PlayMusic(string key)
    {
        if (musicClips.ContainsKey(key))
        {
            audioSource.clip = musicClips[key];
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Music clip not found: " + key);
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}

