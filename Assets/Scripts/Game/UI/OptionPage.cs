using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionPage : MonoBehaviour
{
    [SerializeField]
    private Slider masterSlider;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider foleySlider;
    [SerializeField]
    private Slider sfxSlider;

    public UnityEvent onControlsButtonClick;
    public UnityEvent onBackButtonClick;

    private float throttleTime = 0.1f;
    private float lastSoundInvoke = 1.0f;

    private void Start()
    {
        masterSlider.value = GetVolume(PlayerPrefsKey.MASTER_VOLUME_KEY);
        musicSlider.value = GetVolume(PlayerPrefsKey.MUSIC_VOLUME_KEY);
        foleySlider.value = GetVolume(PlayerPrefsKey.FOLEY_VOLUME_KEY);
        sfxSlider.value = GetVolume(PlayerPrefsKey.SFX_VOLUME_KEY);
    }
    public void changeMasterVolume()
    {
        SetVolume(PlayerPrefsKey.MASTER_VOLUME_KEY, masterSlider.value);
        if (Time.time - lastSoundInvoke >= throttleTime)
        {
            SoundManager.PlaySound(SoundManager.Sound.Slider);
            lastSoundInvoke = Time.time;
        }
        SoundManager.SetMusicVolume(SoundManager.Sound.MenuMusic, 0.75f);
    }

    public void changeMusicVolume()
    {
        SetVolume(PlayerPrefsKey.MUSIC_VOLUME_KEY, musicSlider.value);
        if (Time.time - lastSoundInvoke >= throttleTime)
        {
            SoundManager.PlaySound(SoundManager.Sound.Slider);
            lastSoundInvoke = Time.time;
        }
        SoundManager.SetMusicVolume(SoundManager.Sound.MenuMusic, 0.75f);
    }

    public void changeFoleyVolume()
    {
        SetVolume(PlayerPrefsKey.FOLEY_VOLUME_KEY, foleySlider.value);
        if (Time.time - lastSoundInvoke >= throttleTime)
        {
            SoundManager.PlaySound(SoundManager.Sound.Slider);
            lastSoundInvoke = Time.time;
        }
    }

    public void changeSFXVolume()
    {
        SetVolume(PlayerPrefsKey.SFX_VOLUME_KEY, sfxSlider.value);
        if (Time.time - lastSoundInvoke >= throttleTime) 
        { 
            SoundManager.PlaySound(SoundManager.Sound.Slider);
            lastSoundInvoke = Time.time;
        }
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
