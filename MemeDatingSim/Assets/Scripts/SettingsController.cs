using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Slider textSlider;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    public void SetSliders()
    {
        textSlider.value = Overlord.Instance.player.textSpeed;
        masterSlider.value = Overlord.Instance.player.master;
        musicSlider.value = Overlord.Instance.player.music;
        sfxSlider.value = Overlord.Instance.player.sfx;
    }

    public void ChangeMusicVolume(float v)
    {
        Overlord.Instance.player.music = v;
        SoundManager.Instance.ChangeVolumeOfType("music", v);
    }

    public void ChangeSFXVolume(float v)
    {
        Overlord.Instance.player.sfx = v;
        SoundManager.Instance.ChangeVolumeOfType("sfx", v);
    }

    public void ChangeMasterVolume(float v)
    {
        Overlord.Instance.player.master = v;
        SoundManager.Instance.ChangeVolumeOfType("music", Overlord.Instance.player.music);
        SoundManager.Instance.ChangeVolumeOfType("sfx", Overlord.Instance.player.sfx);
    }

    public void ChangeTextSpeed(float v)
    {
        Overlord.Instance.player.textSpeed = v;
    }
}
