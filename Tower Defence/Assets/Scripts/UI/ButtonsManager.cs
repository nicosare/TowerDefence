using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{
    public Button BoostGameButton;
    private bool isBoostGame;
    private float timeScaleBeforePause;
    public Sprite BoostGameOn;
    public Sprite BoostGameOff;

    public WindowsController windowsController;

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void StartPlayAfterChooseUnit()
    {
        if (FactionsManager.Instance.ChoosenFaction != null)
            SceneManager.LoadScene("Level4");
        else
            Message.Instance.LoadMessage("Выбери фракцию!");
    }

    public void BoostGame()
    {
        if (!isBoostGame)
        {
            Time.timeScale = 2;
            isBoostGame = true;
            BoostGameButton.GetComponent<Image>().sprite = BoostGameOn;
        }
        else
        {
            Time.timeScale = 1;
            isBoostGame = false;
            BoostGameButton.GetComponent<Image>().sprite = BoostGameOff;
        }
    }

    public void PauseGame()
    {
        timeScaleBeforePause = Time.timeScale;
        Time.timeScale = 0;
        windowsController.SetActivePauseMenu(true);
    }

    public void PlayGame()
    {
        Time.timeScale = timeScaleBeforePause;
        windowsController.SetActivePauseMenu(false);
        windowsController.SetInactiveAllWindows();
    }

}
