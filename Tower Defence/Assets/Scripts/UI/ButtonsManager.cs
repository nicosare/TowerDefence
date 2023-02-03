using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{
    public Button BoostGameButton;
    private bool isBoostGame;
    public Sprite BoostGameOn;
    public Sprite BoostGameOff;

    public GameObject PauseMenu;
    public GameObject DefeatMenu;
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
            SceneManager.LoadScene("SampleScene");
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
        Time.timeScale = 0;
        PauseMenu.SetActive(true);    
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        DefeatMenu.SetActive(false);
    }

}
