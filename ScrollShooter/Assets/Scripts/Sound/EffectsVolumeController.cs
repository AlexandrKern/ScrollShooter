using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectsVolumeController : MonoBehaviour
{
    public Slider effectsVolumeSlider;
    public Button toggleEffectsButton;
    public Image toggleEffectsButtonImage;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private void Start()
    {
        float savedVolume = AudioManager.instance.GetEffectsVolume();
        effectsVolumeSlider.value = savedVolume;
        effectsVolumeSlider.onValueChanged.AddListener(OnEffectsVolumeChanged);
        UpdateToggleButton();

        toggleEffectsButton.onClick.AddListener(ToggleEffects);
    }

    private void OnEffectsVolumeChanged(float value)
    {
        AudioManager.instance.SetEffectsVolume(value);
    }

    private void ToggleEffects()
    {
        AudioManager.instance.ToggleEffectsMute();
        UpdateToggleButton();
    }

    private void UpdateToggleButton()
    {
        if (AudioManager.instance.IsEffectsMuted())
        {
            toggleEffectsButtonImage.sprite = soundOffSprite;
        }
        else
        {
            toggleEffectsButtonImage.sprite = soundOnSprite;
        }
    }
}
