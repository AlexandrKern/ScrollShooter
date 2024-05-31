using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceController : MonoBehaviour
{
    private AudioManager _audioManager;
    private AudioSource _audioSource;
    private AudioEffectsManager _effectsManager;

    void Start()
    {
        _effectsManager = AudioEffectsManager.instance;
        _audioManager = AudioManager.instance;
        _audioSource = GetComponent<AudioSource>();

        if (_audioManager != null && _audioSource != null && _effectsManager!=null)
        {
            _audioSource.volume = _audioManager.GetVolume();
            _effectsManager.volume = _effectsManager.GetVolume();
        }
    }
}
