using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioEffectsController : MonoBehaviour
{
    [SerializeField] private Button _onOffSoundButton;
    [SerializeField] private Sprite _onSoundButonsprite;
    [SerializeField] private Sprite _offSoundButonsprite;
    public Slider volumeSlider;
    public List<AudioSource> audioSources;
    private AudioEffectsManager _audioEffectsManager;
    private float _volume;

    void Start()
    {
        _audioEffectsManager = AudioEffectsManager.instance;
        if (audioSources.Count > 0 || _audioEffectsManager != null)
        {
            float savedVolume = _audioEffectsManager.GetVolume();
            audioSources[0].volume = savedVolume;
            volumeSlider.value = audioSources[0].volume;
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }
    }

    private void OnVolumeChanged(float value)
    {
        if (_audioEffectsManager != null)
        {
            foreach (AudioSource source in audioSources)
            {
                _audioEffectsManager.SetVolume(value);

                source.volume = value;
                _volume = value;

                if (source.volume == 0)
                {
                    _onOffSoundButton.image.sprite = _offSoundButonsprite;
                }
                else
                {
                    _onOffSoundButton.image.sprite = _onSoundButonsprite;
                }
            }
        }
    }

    public void SwitchingAudioStates()
    {
        foreach (AudioSource source in audioSources)
        {
            if (source.volume != 0)
            {
                
                source.volume = 0;
                _audioEffectsManager.SetVolume(source.volume);
                _onOffSoundButton.image.sprite = _offSoundButonsprite;
            }
            else
            {
                _onOffSoundButton.image.sprite = _onSoundButonsprite;
                source.volume = _volume;
                _audioEffectsManager.SetVolume(source.volume);
            }
        }
    }

    public void PlaySoundButtonClick()
    {
        audioSources[0].Play();
    }
}
