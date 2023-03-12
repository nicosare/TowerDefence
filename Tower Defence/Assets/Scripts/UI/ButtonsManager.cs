using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{
    public Button BoostGameButton;
    private bool isBoostGame;
    private float timeScaleBeforePause;
    public Sprite BoostGameOn;
    public Sprite BoostGameOff;
    public SoundButton sound;

    public WindowsController windowsController;

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GoToNextLevelScene()
    {
        sound.ChangeSoundsEffect();
        var nameActiveSceneWithHerNumber = SceneManager.GetActiveScene().name.Split("_");
        var nameNextLevel = string.Format("{0}_{1}", nameActiveSceneWithHerNumber[0], int.Parse(nameActiveSceneWithHerNumber[1]) + 1);
        GoToScene(nameNextLevel);
    }

    public void GoToMainMenuScene()
    {
        sound.ChangeSoundsEffect();
        var nameMainMenuScene = "StartMenu";
        GoToScene(nameMainMenuScene);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        sound.ChangeSoundsEffect();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void OpenURL(string URL)
    {
        Application.OpenURL(URL);
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
        sound.ChangeSoundsEffect();
        timeScaleBeforePause = Time.timeScale;
        Time.timeScale = 0;
        windowsController.SetActivePauseMenu(true);
    }

    public void PlayGame()
    {
        sound.ChangeSoundsEffect();
        Time.timeScale = timeScaleBeforePause;
        windowsController.SetActivePauseMenu(false);
        windowsController.SetInactiveAllWindows();
    }

    public void ShowMessageAboutBlockFraction()
    {
        var textRu = "������ ������� �������, ����� ������� ��, �������� �� �������� ������� ��� ������ ��� ������ �������� �������";
        var textEn = "This faction is closed, to open it, go through all the levels for the open faction or buy the desired faction";
        if (LocalizationSettings.Instance.GetSelectedLocale() == LocalizationSettings.AvailableLocales.Locales[0])
            Message.Instance.LoadMessage(textEn, 5);
        else
            Message.Instance.LoadMessage(textRu, 5);
    }
}
