using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowsController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject defeatMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private Button[] buttonsNeedDisable;
    private AudioSource audioSourceBackground;
    private AudioSource audioSource;
    [SerializeField] private AudioClip soundWin;
    [SerializeField] private AudioClip soundLose;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSourceBackground = BackgroundMusicController.Instance.GetComponent<AudioSource>();
        audioSourceBackground.volume = 0.2f;
    }

    public void SetActivePauseMenu(bool isActive)
    {
        SetActive(pauseMenu, isActive);
    }

    public void SetActiveDefeatMenu(bool isActive)
    {
        audioSourceBackground.volume = 0.1f;
        SetActive(defeatMenu, isActive);
        audioSource.PlayOneShot(soundLose);
    }

    public void SetActiveWinMenu(bool isActive)
    {
        audioSourceBackground.volume = 0.1f;
        SetActive(winMenu, isActive);
        audioSource.PlayOneShot(soundWin);
    }

    public void SetInactiveAllWindows()
    {
        SetActive(new[] { pauseMenu, defeatMenu, winMenu }, false);
    }

    private void SetActive(GameObject menu, bool isActive)
    {
        menu.SetActive(isActive);
        foreach (var button in buttonsNeedDisable)
            button.interactable = !isActive;    
    }

    private void SetActive(GameObject[] menus, bool isActive)
    {
        foreach (var menu in menus)
            SetActive(menu, isActive);
    }
}
