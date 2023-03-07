using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Location[] Locations;

    public List<Button> buttons;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    public void UpdateButtons()
    {
        var unblockedLevels = Progress.Instance.GetUnblockedLevelsByNameFraction(FactionsManager.Instance.ChoosenFaction.NameFaction);
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].GetComponent<Button>().onClick.AddListener(ChooseLevel);

            buttons[i].interactable = unblockedLevels[i];
            buttons[i].transform.GetChild(0).gameObject.SetActive(unblockedLevels[i]);
        }
    }
    private void ChooseLevel()
    {
        var levelIndex = 1 + buttons.IndexOf(EventSystem.current.currentSelectedGameObject.GetComponent<Button>());
        SceneManager.LoadScene("Level_" + levelIndex);
    }

    public void UnblockLevel(Scene scene)
    {
        var levelIndex = int.Parse(scene.name.Split('_')[1]);
        Progress.Instance.UnblockLevelByNameFraction(FactionsManager.Instance.ChoosenFaction.NameFaction, levelIndex);
    }
}
