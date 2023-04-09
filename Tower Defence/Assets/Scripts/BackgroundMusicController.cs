using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicController : MonoBehaviour
{
    public bool OnMusic;
    private AudioSource audioSource;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip fightMusic;
    private string[] nameCurrentScene;
    private bool isFirstLaunch;
    public static BackgroundMusicController Instance;
    private SoundButton sound;

    public void SoundOff()
    {
        if (OnMusic)
        {
            sound = FindObjectOfType<SoundButton>();
            sound.ChangeVolumeMusic();
        }
    }

    public void SoundOn()
    {
        if (sound != null)
            sound.ChangeVolumeMusic();
    }

    private void Awake()
    {
        OnMusic = true;
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            isFirstLaunch = true;
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
            SceneManager.activeSceneChanged += ChangePlayedMusic;
            audioSource.clip = menuMusic;
            audioSource.Play();
        }
    }

    private void ChangePlayedMusic(Scene currentScene, Scene nextScene)
    {
        var nameNextScene = nextScene.name.Split("_");

        if (!isFirstLaunch)
        {
            if (nameCurrentScene[0] != "Level" && nameNextScene[0] == "Level" && nameNextScene[1] != "Tutorial")
            {
                audioSource.clip = fightMusic;
                audioSource.Play();
            }
            else if ((nameCurrentScene[0] == "Level" && nameNextScene[0] != "Level") || (nameCurrentScene[0] == "Level" && nameNextScene[1] == "Tutorial"))
            {
                audioSource.clip = menuMusic;
                audioSource.Play();
            }
        }
        else
            isFirstLaunch = false;
        nameCurrentScene = nextScene.name.Split("_");
    }

    public void ChangeMainMusic()
    {
        if (audioSource.clip == fightMusic)
            audioSource.clip = menuMusic;
        else
            audioSource.clip = fightMusic;
        audioSource.Play();
    }
}