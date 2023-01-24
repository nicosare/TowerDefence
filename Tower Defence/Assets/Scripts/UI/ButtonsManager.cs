using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{
    public void GoToScene(string sceneName) => SceneManager.LoadScene(sceneName);

    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void StartPlayAfterChooseUnit()
    {
        if (FactionsManager.Instance.ChoosenFaction != null)
            SceneManager.LoadScene("SampleScene");
        else
            Message.Instance.LoadMessage("Выбери фракцию!");
    }

}
