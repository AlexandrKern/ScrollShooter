using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour, ISoundManager
{
    public static EffectsManager instance;
    private float volume = 1.0f;
    private bool isMuted = false;
    private AudioSource audioSource;
    private Dictionary<string, AudioClip> effectClips;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
            effectClips = new Dictionary<string, AudioClip>();

            // Load saved volume
            volume = PlayerPrefs.GetFloat("EffectsVolume", 1.0f);
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
        PlayerPrefs.SetFloat("EffectsVolume", volume);
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

    public void AddEffectClip(string key, AudioClip clip)
    {
        if (!effectClips.ContainsKey(key))
        {
            effectClips.Add(key, clip);
        }
    }

    public void PlayEffect(string key)
    {
        if (effectClips.ContainsKey(key))
        {
            audioSource.PlayOneShot(effectClips[key]);
        }
        else
        {
            Debug.LogWarning("Effect clip not found: " + key);
        }
    }
}
