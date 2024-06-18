using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public MusicManager musicManager;
    public EffectsManager effectsManager;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (musicManager != null)
        {
            musicManager.SetVolume(volume);
        }
    }

    public void SetEffectsVolume(float volume)
    {
        if (effectsManager != null)
        {
            effectsManager.SetVolume(volume);
        }
    }

    public float GetMusicVolume()
    {
        return musicManager != null ? musicManager.GetVolume() : 1.0f;
    }

    public float GetEffectsVolume()
    {
        return effectsManager != null ? effectsManager.GetVolume() : 1.0f;
    }

    public void ToggleMusicMute()
    {
        if (musicManager != null)
        {
            musicManager.ToggleMute();
        }
    }

    public void ToggleEffectsMute()
    {
        if (effectsManager != null)
        {
            effectsManager.ToggleMute();
        }
    }

    public bool IsMusicMuted()
    {
        return musicManager != null && musicManager.IsMuted();
    }

    public bool IsEffectsMuted()
    {
        return effectsManager != null && effectsManager.IsMuted();
    }

    public void AddMusicClip(string key, AudioClip clip)
    {
        if (musicManager != null)
        {
            musicManager.AddMusicClip(key, clip);
        }
    }

    public void PlayMusic(string key)
    {
        if (musicManager != null)
        {
            musicManager.PlayMusic(key);
        }
    }

    public void StopMusic()
    {
        if (musicManager != null)
        {
            musicManager.StopMusic();
        }
    }

    public void AddEffectClip(string key, AudioClip clip)
    {
        if (effectsManager != null)
        {
            effectsManager.AddEffectClip(key, clip);
        }
    }

    public void PlayEffect(string key)
    {
        if (effectsManager != null)
        {
            effectsManager.PlayEffect(key);
        }
    }
}
