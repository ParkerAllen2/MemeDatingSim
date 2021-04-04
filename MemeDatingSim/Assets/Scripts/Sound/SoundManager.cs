using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    protected SoundManager() { }
    public Sound[] sounds;
    Dictionary<string, float> soundTimerDictionary;

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
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, s => s.soundName == name);
        if(sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found");
            return;
        }
        if (!CanPlaySound(sound)) return;

        sound.source.Play();
    }

    public void Stop(string name)
    {
        Sound sound = Array.Find(sounds, s => s.soundName == name);
        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found");
            return;
        }

        sound.source.Stop();
    }

    public void StopType(string type)
    {
        Sound[] sound = Array.FindAll(sounds, s => s.type == type);
        foreach(Sound s in sound)
            s.source.Stop();
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

    bool CanPlaySound(Sound sound)
    {
        if (!soundTimerDictionary.ContainsKey(sound.soundName))
        {
            return true;
        }
        if(soundTimerDictionary[sound.name] < Time.time)
        {
            soundTimerDictionary[sound.name] = Time.time + sound.clip.length;
            return true;
        }
        return false;
    }
}
