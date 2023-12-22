using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        MenuMusic,
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
        Build,
        TownFoley,
    }

    private static List<AudioSource> pausableAudioSources;
    private static List<AudioSource> unPausableAudioSources;
    private static Dictionary<Sound, AudioSource> musicSources;

    private static bool isInstantiated = false;

    public static void Initialize()
    {
        if (!isInstantiated)
        {
            isInstantiated = true;
            pausableAudioSources = new List<AudioSource>();
            unPausableAudioSources = new List<AudioSource>();
            musicSources = new Dictionary<Sound, AudioSource>();
        }
    }

    public static void InitiateMenuMusic()
    {
        Initialize();

        Sound sound = Sound.MenuMusic;
        GameObject musicGameObject = new GameObject("Music");
        AudioSource audio = musicGameObject.AddComponent<AudioSource>();
        audio.clip = GetAudioClip(sound);
        audio.volume = 0;
        audio.loop = true;
        audio.Play();
        musicSources[sound] = audio;
    }

    public static void InitiateGameMusic()
    {
        Initialize();

        Sound[] sounds = { Sound.BassMusic, Sound.BackgroundMusic, Sound.LeadMusic, Sound.PercutionMusic };

        foreach(Sound sound in sounds)
        {
            GameObject musicGameObject = new GameObject("Music");
            AudioSource audio = musicGameObject.AddComponent<AudioSource>();
            audio.clip = GetAudioClip(sound);
            audio.volume = 0;
            audio.loop = true;
            audio.Play();
            musicSources[sound] = audio;
        }
    }

    public static void PlayFoley(Sound sound, Vector3 position, float maxDistance = 25f, float spatialBlend = 1f,
                                 AudioRolloffMode rolloffMode = AudioRolloffMode.Linear, float dopplerLevel = 0f)
    {
        Initialize();

        GameObject foleyGameObject = CreateSound(sound);

        foleyGameObject.transform.position = position;
        AudioSource audio = foleyGameObject.GetComponent<AudioSource>();
        audio.volume = PlayerPrefs.GetFloat(PlayerPrefsKey.FOLEY_VOLUME_KEY, 0.15f) * PlayerPrefs.GetFloat(PlayerPrefsKey.MASTER_VOLUME_KEY, 1f);
        audio.maxDistance = maxDistance;
        audio.spatialBlend = spatialBlend;
        audio.rolloffMode = rolloffMode;
        audio.dopplerLevel = dopplerLevel;
        musicSources[sound] = audio;
    }

    public static GameObject PlaySound(Sound sound, Vector3 position, bool startAtRandomTime = false, float maxDistance = 25f, float spatialBlend = 1f,
                                       AudioRolloffMode rolloffMode = AudioRolloffMode.Linear, float dopplerLevel = 0f)
    {
        Initialize();

        GameObject soundGameObject = CreateSound(sound, startAtRandomTime);

        if (soundGameObject != null)
        {
            soundGameObject.transform.position = position;
            AudioSource audio = soundGameObject.GetComponent<AudioSource>();
            audio.maxDistance = maxDistance;
            audio.spatialBlend = spatialBlend;
            audio.rolloffMode = rolloffMode;
            audio.dopplerLevel = dopplerLevel;
            pausableAudioSources.Add(audio);
            Object.Destroy(soundGameObject, audio.clip.length * GetRepeatTime(sound));
        }
        return soundGameObject;
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
            audio.loop = false;
            return soundGameObject;
        }
        return null;
    }

    public static IEnumerator MusicVolumeFade(Dictionary<Sound, float> newVolumes, float fadeTime = 2f)
    {
        Dictionary<Sound, float> initialVolumes = new Dictionary<Sound, float>();
        foreach (Sound sound in newVolumes.Keys)
        {
            if (musicSources != null && musicSources.ContainsKey(sound) && musicSources[sound]) initialVolumes.Add(sound, musicSources[sound].volume);
        }

        float time = 0f;
        while (time < fadeTime && initialVolumes.Keys.Count > 0)
        {
            time += Time.deltaTime;

            foreach (Sound sound in initialVolumes.Keys)
            {
                musicSources[sound].volume = Mathf.Lerp(initialVolumes[sound], newVolumes[sound], time / fadeTime) * PlayerPrefs.GetFloat(PlayerPrefsKey.MUSIC_VOLUME_KEY, 0.15f) * PlayerPrefs.GetFloat(PlayerPrefsKey.MASTER_VOLUME_KEY, 1f);
            }

            yield return null;
        }

        foreach (Sound sound in initialVolumes.Keys)
        {
            musicSources[sound].volume = newVolumes[sound] * PlayerPrefs.GetFloat(PlayerPrefsKey.MUSIC_VOLUME_KEY, 0.15f) * PlayerPrefs.GetFloat(PlayerPrefsKey.MASTER_VOLUME_KEY, 1f);
        }
        yield break;
    }

    public static void SetMusicVolume(Sound sound, float newVolume)
    {
        float adjustedNewVolume = newVolume * PlayerPrefs.GetFloat(PlayerPrefsKey.MUSIC_VOLUME_KEY, 0.15f) * PlayerPrefs.GetFloat(PlayerPrefsKey.MASTER_VOLUME_KEY, 1f);
        if (musicSources != null && musicSources.ContainsKey(sound) && musicSources[sound] != null) musicSources[sound].volume = adjustedNewVolume;
    }

    public static void PauseSounds()
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

        foreach (Sound sound in musicSources.Keys)
        {
            if (musicSources[sound] != null)
            {
                musicSources[sound].Pause();
            }
        }
    }

    public static void ResumeSounds()
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

        foreach (Sound sound in musicSources.Keys)
        {
            if (musicSources[sound] != null)
            {
                musicSources[sound].Play();
            }
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
