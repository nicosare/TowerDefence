using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundButton : MonoBehaviour
{
    public AudioMixerGroup Mixer;
    public Sprite SoundGameOn;
    public Sprite SoundGameOff;
    private bool isSoundOff;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeVolumeMusic()
    {
        if (isSoundOff)
            SoundOn();
        else
            SoundOff();
    }

    private void SoundOn()
    {
        isSoundOff = false;
        GetComponent<Image>().sprite = SoundGameOn;
        Mixer.audioMixer.SetFloat("MasterVolume", 0);
    }

    private void SoundOff()
    {
        isSoundOff = true;
        GetComponent<Image>().sprite = SoundGameOff;
        Mixer.audioMixer.SetFloat("MasterVolume", -80);
    }
}
