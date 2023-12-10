using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionPage : MonoBehaviour
{
    [SerializeField]
    private Slider sfxSlider; 
    [SerializeField]
    private Slider musicSlider;
    
    public UnityEvent onMusicVolumeChanged;
    public UnityEvent onSFXVolumeChanged;
    public UnityEvent onControlsButtonClick;
    public UnityEvent onBackButtonClick;


    private void Start()
    {
        sfxSlider.value = GetVolume(PlayerPrefsKey.SFX_VOLUME_KEY);
        musicSlider.value = GetVolume(PlayerPrefsKey.MUSIC_VOLUME_KEY);
    }

    public void changeSFXVolume()
    {
        SetVolume(PlayerPrefsKey.SFX_VOLUME_KEY, sfxSlider.value);
        onSFXVolumeChanged.Invoke();
    }
    public void changeMusicVolume()
    {
        SetVolume(PlayerPrefsKey.MUSIC_VOLUME_KEY, musicSlider.value);
        onSFXVolumeChanged.Invoke();
    }
    public void controlsButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onControlsButtonClick.Invoke();
    }   
    public void backButtonClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuSound);
        onBackButtonClick.Invoke();
    }

    private void SetVolume(string key, float volume)
    {
        float ajustedVolume = Mathf.Lerp(0, 1, Mathf.Pow(volume, 2f));
        PlayerPrefs.SetFloat(key, ajustedVolume);
    }
    private float GetVolume(string key)
    {
        float volume = PlayerPrefs.GetFloat(key, 0.15f);
        return Mathf.Pow(Mathf.InverseLerp(0, 1, volume), 1f / 2f);
    }
}
