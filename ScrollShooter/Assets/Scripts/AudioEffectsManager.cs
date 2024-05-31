using UnityEngine;

public class AudioEffectsManager : MonoBehaviour
{

    public static AudioEffectsManager instance;

    public float volume = 1.0f;

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

    public void SetVolume(float newVolume)
    {
        volume = newVolume;
        PlayerPrefs.SetFloat("MusicEffectValume", volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        if (PlayerPrefs.HasKey("MusicEffectValume"))
        {
            volume = PlayerPrefs.GetFloat("MusicEffectValume");
        }
        return volume;
    }

}
