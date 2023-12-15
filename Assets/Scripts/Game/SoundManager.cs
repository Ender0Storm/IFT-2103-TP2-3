using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        BackgroundMusic,
        BassMusic,
        LeadMusic,
        PercutionMusic,
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
        Walking,
        Error,
        Build
    }

    private static Dictionary<Sound, float> soundTimerDict;
    private static List<AudioSource> audioSources;
    private static List<AudioSource> pausableAudioSources;
    private static List<AudioSource> unPausableAudioSources;
    private static Dictionary<Sound, AudioSource> musicSources;

    private static bool isInstantiated = false;

    public static void Initialize()
    {
        if (!isInstantiated)
        {
            isInstantiated = true;
            soundTimerDict = new Dictionary<Sound, float>();
            audioSources = new List<AudioSource>();
            pausableAudioSources = new List<AudioSource>();
            unPausableAudioSources = new List<AudioSource>();
            musicSources = new Dictionary<Sound, AudioSource>();
        }
    }

    public static void PlayMusic(Sound sound)
    {
        // TODO: Finish music section with stop conditions, looping and transitions
        if (!musicSources.ContainsKey(sound))
        {
            GameObject musicGameObject = new GameObject("Music");
            AudioSource audio = musicGameObject.AddComponent<AudioSource>();
            audio.clip = GetAudioClip(sound);
            audio.volume = PlayerPrefs.GetFloat(PlayerPrefsKey.MUSIC_VOLUME_KEY, 0.15f) * PlayerPrefs.GetFloat(PlayerPrefsKey.MASTER_VOLUME_KEY, 1f);
            audio.Play();
            musicSources[sound] = audio;
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
            audio.volume = PlayerPrefs.GetFloat(PlayerPrefsKey.SFX_VOLUME_KEY, 0.15f) * PlayerPrefs.GetFloat(PlayerPrefsKey.MASTER_VOLUME_KEY, 1f);
            audio.maxDistance = maxDistance;
            audio.spatialBlend = spatialBlend;
            audio.rolloffMode = rolloffMode;
            audio.dopplerLevel = dopplerLevel;
            audio.Play();
            audioSources.Add(audio);
        }
    }
    public static GameObject PlaySound(Sound sound, bool startAtRandomTime = false)
    {
        Initialize();

        GameObject soundGameObject = CreateSound(sound, startAtRandomTime);

        if (soundGameObject != null)
        {
            AudioSource audio = soundGameObject.GetComponent<AudioSource>();
            pausableAudioSources.Add(audio);
            Object.Destroy(soundGameObject, audio.clip.length * GetRepeatTime(sound));
        }
        return soundGameObject;
    }

    public static GameObject PlayUnPausableSound(Sound sound)
    {
        Initialize();

        GameObject soundGameObject = CreateSound(sound);

        if (soundGameObject != null)
        {
            AudioSource audio = soundGameObject.GetComponent<AudioSource>();
            TimedDestruction autoDestruction = soundGameObject.AddComponent<TimedDestruction>();
            unPausableAudioSources.Add(audio);
            autoDestruction.DeleteIn(audio.clip.length * GetRepeatTime(sound));
        }
        return soundGameObject;
    }

    private static GameObject CreateSound(Sound sound, bool startAtRandomTime = false)
    {
        Initialize();

        AudioClip clip = GetAudioClip(sound);
        if (clip != null)
        {
            GameObject soundGameObject = new GameObject(sound + " Sound");
            AudioSource audio = soundGameObject.AddComponent<AudioSource>();
            audio.clip = clip;
            audio.volume = PlayerPrefs.GetFloat(PlayerPrefsKey.SFX_VOLUME_KEY, 0.15f) * PlayerPrefs.GetFloat(PlayerPrefsKey.MASTER_VOLUME_KEY, 1f);
            if (startAtRandomTime)
            {
                audio.time = Random.Range(0f, audio.clip.length);
            }
            audio.Play();
            audio.loop = true;
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

    public static void PauseSFX()
    {
        Initialize();
        for (int i = 0; i < pausableAudioSources.Count; i++)
        {
            AudioSource audio = pausableAudioSources[i];
            if (audio != null)
            {
                audio.Pause();
            }
        }
    }
    public static void ResumeSFX()
    {
        Initialize();
        for (int i = 0; i < pausableAudioSources.Count; i++)
        {
            AudioSource audio = pausableAudioSources[i];
            if (audio != null)
            { 
                audio.Play();
            }
        }
    }

    private static void CleanAudioSources()
    {
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
