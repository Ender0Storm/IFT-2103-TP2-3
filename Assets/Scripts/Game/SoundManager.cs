using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        BubblingSound
    }

    private static Dictionary<Sound, float> soundTimerDict;
    private static List<AudioSource> audioSources;
    private static AudioSource currentMusic;
    private static AudioSource transitionMusic;

    // TODO: Connect to menus
    private static float masterVolume;
    private static float musicVolume;
    private static float soundVolume;

    public static void Initialize()
    {
        soundTimerDict = new Dictionary<Sound, float>();
        audioSources = new List<AudioSource>();
        masterVolume = 1f;
        musicVolume = 1f;
        soundVolume = 1f;
    }

    public static void PlayMusic(Sound sound)
    {
        // TODO: Finish music section with stop conditions, looping and transitions
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audio = soundGameObject.AddComponent<AudioSource>();
            audio.clip = GetAudioClip(sound);
            audio.volume = masterVolume * soundVolume;
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
            audio.volume = masterVolume * soundVolume;
            audio.maxDistance = maxDistance;
            audio.spatialBlend = spatialBlend;
            audio.rolloffMode = rolloffMode;
            audio.dopplerLevel = dopplerLevel;
            audio.Play();
            audioSources.Add(audio);
        }
    }

    public static void PlaySound(Sound sound)
    {
        CleanAudioSources();
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audio = soundGameObject.AddComponent<AudioSource>();
            audio.clip = GetAudioClip(sound);
            audio.volume = masterVolume * soundVolume;
            audio.Play();
            audioSources.Add(audio);
        }
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
