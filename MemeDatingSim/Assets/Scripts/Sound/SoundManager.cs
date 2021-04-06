using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    protected SoundManager() { }

    public Sound[] sounds;
    Dictionary<string, float> soundTimerDictionary;

    Sound currentTheme;
    FadeAudio fadeAudio;

    public override void Awake()
    {
        base.Awake();

        soundTimerDictionary = new Dictionary<string, float>();
        foreach (Sound s in sounds)
        {
            AudioSource tmp = gameObject.AddComponent<AudioSource>();
            tmp.clip = s.clip;
            tmp.volume = s.maxVolume;
            tmp.pitch = s.pitch;
            tmp.loop = s.isLoop;
            s.source = tmp;
        }

        ChangeVolumeOfType("music", Overlord.Instance.player.music);
        ChangeVolumeOfType("sfx", Overlord.Instance.player.sfx);

        fadeAudio = gameObject.AddComponent<FadeAudio>();
    }

    public bool Play(string name)
    {
        Sound sound = Array.Find(sounds, s => s.soundName == name);
        if (sound == null) return false;
        if (!CanPlaySound(sound)) return true;

        sound.source.Play();
        return true;
    }

    public bool Stop(string name)
    {
        Sound sound = Array.Find(sounds, s => s.soundName == name);
        if (sound == null) return false;

        sound.source.Stop();
        return true;
    }

    public bool StartPlayingTheme(string name, float duration = 2)
    {
        if(currentTheme != null)
        {
            if (name.Equals(currentTheme.soundName)) return true;

            fadeAudio.StartFading(currentTheme, duration, false);
        }

        Sound nextTheme = Array.Find(sounds, s => s.soundName == name);
        if(nextTheme == null) return false;

        nextTheme.source.Play();
        fadeAudio.StartFading(nextTheme, duration, true);
        currentTheme = nextTheme;
        return true;
    }

    public void ChangeVolumeOfType(string type, float volume)
    {
        Sound[] sound = Array.FindAll(sounds, s => s.type == type);
        foreach (Sound s in sound)
        {
            float vol = s.maxVolume * volume * Overlord.Instance.player.master;
            s.source.volume = vol;
        }
    }

    public float GetMaxVolume(Sound s)
    {
        float rtn = s.maxVolume * Overlord.Instance.player.master;
        if (s.type == "music")
        {
            rtn *= Overlord.Instance.player.music;
        }
        else if (s.type == "sfx")
        {
            rtn *= Overlord.Instance.player.sfx;
        }
        return rtn;
    }

    bool CanPlaySound(Sound sound)
    {
        if (!soundTimerDictionary.ContainsKey(sound.soundName))
        {
            return true;
        }
        if(soundTimerDictionary[sound.soundName] < Time.time)
        {
            soundTimerDictionary[sound.soundName] = Time.time + sound.clip.length;
            return true;
        }
        return false;
    }
}