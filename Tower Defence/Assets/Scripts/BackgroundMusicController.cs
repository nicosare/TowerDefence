using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicController : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip fightMusic;
    private string[] nameCurrentScene;
    private bool isFirstLaunch;
    public static BackgroundMusicController Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
        SceneManager.activeSceneChanged += ChangePlayedMusic;
        audioSource.clip = menuMusic;
        audioSource.Play();
        isFirstLaunch = true;
    }

    private void ChangePlayedMusic(Scene currentScene, Scene nextScene)
    {
        var nameNextScene = nextScene.name.Split("_");

        if (!isFirstLaunch)
        {
            if (nameCurrentScene[0] != "Level" && nameNextScene[0] == "Level")
            {
                audioSource.clip = fightMusic;
                audioSource.Play();
            }
            else if (nameCurrentScene[0] == "Level" && nameNextScene[0] != "Level")
            {
                audioSource.clip = menuMusic;
            }
        }
        else
            isFirstLaunch = false;
        nameCurrentScene = nextScene.name.Split("_");
    }
}