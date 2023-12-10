using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        MenuMusic,
        BuildingMusic,
        WaveMusic,
        BossWaveMusic,
        MenuSound,
        CheckboxSound,
        Pause,
        UnPause,
        Slider,
        GameOver,
        CannonShot,
        TurretShot,
        SniperShot,
        LoseLife,
        Walking
    }

    private static Dictionary<Sound, float> soundTimerDict;
    private static List<AudioSource> audioSources;
    private static AudioSource currentMusic;
    private static AudioSource transitionMusic;

    private static bool isInstantiated = false;

    public static void Initialize()
    {
        if (!isInstantiated)
        {
            isInstantiated = true;
            soundTimerDict = new Dictionary<Sound, float>();
            audioSources = new List<AudioSource>();
        }
    }

    public static void PlayMusic(Sound sound)
    {
        // TODO: Finish music section with stop conditions, looping and transitions
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audio = soundGameObject.AddComponent<AudioSource>();
            audio.clip = GetAudioClip(sound);
            audio.volume = PlayerPrefs.GetFloat(PlayerPrefsKey.MUSIC_VOLUME_KEY, 0.15f);
            audio.Play();
            currentMusic = audio;
        }
    }

    public static void PlaySound(Sound sound, Vector3 position, float maxDistance = 100f, float spatialBlend = 1f,
                                 AudioRolloffMode rolloffMode = AudioRolloffMode.Linear, float dopplerLevel = 0f)
    {
        CleanAudioSources();
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = position;
            AudioSource audio = soundGameObject.AddComponent<AudioSource>();
            audio.clip = GetAudioClip(sound);
            audio.volume = PlayerPrefs.GetFloat(PlayerPrefsKey.SFX_VOLUME_KEY, 0.15f);
            audio.maxDistance = maxDistance;
            audio.spatialBlend = spatialBlend;
            audio.rolloffMode = rolloffMode;
            audio.dopplerLevel = dopplerLevel;
            audio.Play();
            audioSources.Add(audio);
        }
    }

    public static GameObject PlaySound(Sound sound)
    {
        CleanAudioSources();
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject(sound + " Sound");
            AudioSource audio = soundGameObject.AddComponent<AudioSource>();
            audio.clip = GetAudioClip(sound);
            audio.volume = PlayerPrefs.GetFloat(PlayerPrefsKey.SFX_VOLUME_KEY, 0.15f);
            audio.Play();
            audio.loop = true;
            audioSources.Add(audio);
            GameObject.Destroy(soundGameObject, audio.clip.length * GetRepeatTime(sound));
            return soundGameObject;
        }
        return null;
    }

    private static bool CanPlaySound(Sound sound)
    {
        float repeatTime = GetRepeatTime(sound);

        if (repeatTime > 0)
        {
            if (!soundTimerDict.ContainsKey(sound))
            {
                soundTimerDict.Add(sound, Time.time - (2 * repeatTime));
            }

            if (soundTimerDict[sound] + repeatTime <= Time.time)
            {
                soundTimerDict[sound] = Time.time;
                return true;
            }

            return false;
        }

        return true;
    }

    private static void CleanAudioSources()
    {
        if(!isInstantiated) {
            Initialize();
        }
        audioSources.RemoveAll(audioSource => audioSource == null);
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying) Object.Destroy(audioSource.gameObject);
        }
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClips)
        {
            if (soundAudioClip.sound == sound) return soundAudioClip.audioClip;
        }
        Debug.LogError($"Sound {sound} not found");
        return null;
    }

    private static float GetRepeatTime(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClips)
        {
            if (soundAudioClip.sound == sound) return soundAudioClip.repeatTime;
        }
        Debug.LogError($"Sound {sound} not found");
        return 0;
    }
}
