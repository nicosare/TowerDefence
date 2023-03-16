using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowsController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject defeatMenu;
    [SerializeField] private GameObject BackgroundBlackout;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject winMenuWithAchievment;
    [SerializeField] private Button[] buttonsNeedDisable;
    private AudioSource audioSourceBackground;
    private AudioSource audioSource;
    [SerializeField] private AudioClip soundWin;
    [SerializeField] private AudioClip soundLose;

    [SerializeField] private GameObject achievmentHumans;
    [SerializeField] private GameObject achievmentElves;
    [SerializeField] private GameObject achievmentGnomes;
    [SerializeField] private GameObject achievmentGoblins;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSourceBackground = BackgroundMusicController.Instance.GetComponent<AudioSource>();
        audioSourceBackground.volume = 0.2f;
    }

    public void SetActivePauseMenu(bool isActive)
    {
        SetActive(pauseMenu, isActive);
        SetActive(BackgroundBlackout, isActive);
    }

    public void SetActiveDefeatMenu(bool isActive)
    {
        audioSourceBackground.volume = 0.1f;
        SetActive(defeatMenu, isActive);
        SetActive(BackgroundBlackout, isActive);
        audioSource.PlayOneShot(soundLose);
    }

    public void SetActiveWinMenu(bool isActive)
    {
        audioSourceBackground.volume = 0.1f;
        SetActive(winMenu, isActive);
        SetActive(BackgroundBlackout, isActive);
        audioSource.PlayOneShot(soundWin);
    }

    public void SetActiveWinMenuWithAchievment(bool isActive)
    {
        audioSourceBackground.volume = 0.1f;
        SetActive(winMenuWithAchievment, isActive);
        SetActive(BackgroundBlackout, isActive);
        audioSource.PlayOneShot(soundWin);
        if (FactionsManager.Instance.ChoosenFaction.name == "Люди" || FactionsManager.Instance.ChoosenFaction.name == "Humans")
        {
            achievmentHumans.SetActive(true);
        }
        else if (FactionsManager.Instance.ChoosenFaction.name == "Эльфы" || FactionsManager.Instance.ChoosenFaction.name == "Elves")
        {
            achievmentElves.SetActive(true);
        }
        else if (FactionsManager.Instance.ChoosenFaction.name == "Гномы" || FactionsManager.Instance.ChoosenFaction.name == "Dwarves")
        {
            achievmentGnomes.SetActive(true);
        }
        else if (FactionsManager.Instance.ChoosenFaction.name == "Гоблины" || FactionsManager.Instance.ChoosenFaction.name == "Goblins")
        {
            achievmentGoblins.SetActive(true);
        }
    }

    public void SetInactiveAllWindows()
    {
        SetActive(new[] { pauseMenu, defeatMenu, winMenu, BackgroundBlackout }, false);
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
