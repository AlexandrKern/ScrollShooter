using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeController : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Button toggleMusicButton;
    public Image toggleMusicButtonImage;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private void Start()
    {
        float savedVolume = AudioManager.instance.GetMusicVolume();
        musicVolumeSlider.value = savedVolume;
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        UpdateToggleButton();

        toggleMusicButton.onClick.AddListener(ToggleMusic);
    }

    private void OnMusicVolumeChanged(float value)
    {
        AudioManager.instance.SetMusicVolume(value);
    }

    private void ToggleMusic()
    {
        AudioManager.instance.ToggleMusicMute();
        UpdateToggleButton();
    }

    private void UpdateToggleButton()
    {
        if (AudioManager.instance.IsMusicMuted())
        {
            toggleMusicButtonImage.sprite = soundOffSprite;
        }
        else
        {
            toggleMusicButtonImage.sprite = soundOnSprite;
        }
    }

}
