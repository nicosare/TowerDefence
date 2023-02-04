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

    public void SetActivePauseMenu(bool isActive)
    {
        SetActive(pauseMenu, isActive);
    }

    public void SetActiveDefeatMenu(bool isActive)
    {
        SetActive(defeatMenu, isActive);
    }

    public void SetActiveWinMenu(bool isActive)
    {
        SetActive(winMenu, isActive);
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
