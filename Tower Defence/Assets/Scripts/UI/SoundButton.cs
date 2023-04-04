using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class SoundButton : MonoBehaviour
{
    public AudioMixerGroup Mixer;
    public Sprite SoundGameOn;
    public Sprite SoundGameOff;
    private bool isSoundOff;
    private bool isSoundsEffectOff;

    private void Start()
    {
        if (BackgroundMusicController.Instance.OnMusic == false)
            ChangeVolumeMusic();
    }

    public void ChangeVolumeMusic()
    {
        if (isSoundOff)
        {
            AllSoundsOn();
            BackgroundMusicController.Instance.OnMusic = true;
        }
        else
        {
            AllSoundsOff();
            BackgroundMusicController.Instance.OnMusic = false;
        }
    }

    public void ChangeSoundsEffect()
    {
        if (isSoundsEffectOff)
            SoundsEffectOn();
        else
            SoundsEffectOff();
    }

    private void SoundsEffectOff()
    {
        isSoundsEffectOff = true;
        Mixer.audioMixer.SetFloat("EffectsVolume", -80);
    }

    private void SoundsEffectOn()
    {
        isSoundsEffectOff = false;
        Mixer.audioMixer.SetFloat("EffectsVolume", 0);
    }

    private void AllSoundsOn()
    {
        isSoundOff = false;
        GetComponent<Image>().sprite = SoundGameOn;
        Mixer.audioMixer.SetFloat("MasterVolume", 0);
    }

    private void AllSoundsOff()
    {
        isSoundOff = true;
        GetComponent<Image>().sprite = SoundGameOff;
        Mixer.audioMixer.SetFloat("MasterVolume", -80);
    }
}
