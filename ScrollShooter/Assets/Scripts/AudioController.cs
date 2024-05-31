using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioController : MonoBehaviour
{
    [SerializeField] private Button _onOffSoundButton;
    [SerializeField] private Sprite _onSoundButonsprite;
    [SerializeField] private Sprite _offSoundButonsprite;
    public Slider volumeSlider;
    public List<AudioSource> audioSources;
    private AudioManager _audioManager;
    private float _volume;

    void Start()
    {
        _audioManager = AudioManager.instance;

        if (audioSources.Count > 0|| _audioManager != null)
        {
            float savedVolume = _audioManager.GetVolume();
            audioSources[0].volume = savedVolume;
            volumeSlider.value = audioSources[0].volume;
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }
    }

    private void OnVolumeChanged(float value)
    {
        if (_audioManager != null)
        {
            foreach (AudioSource source in audioSources)
            {
                _audioManager.SetVolume(value);

                source.volume = value;
                _volume = value;

                if (source.volume == 0 || !source.isPlaying)
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
                _audioManager.SetVolume(source.volume);
                _onOffSoundButton.image.sprite = _offSoundButonsprite;
            }
            else
            {
                _onOffSoundButton.image.sprite = _onSoundButonsprite;
                source.volume = _volume;
                _audioManager.SetVolume(source.volume);
            }
        }
    }
}
