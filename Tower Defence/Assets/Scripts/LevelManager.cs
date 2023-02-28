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
    public List<int> UnblockedLevels;

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
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].GetComponent<Button>().onClick.AddListener(ChooseLevel);
            
            //if (i == 0 || UnblockedLevels.Contains(i))
            //{
            //    buttons[i].interactable = true;
            //    buttons[i].transform.GetChild(0).gameObject.SetActive(true);
            //}
            //else
            //{
            //    buttons[i].interactable = false;
            //    buttons[i].transform.GetChild(0).gameObject.SetActive(false);
            //}
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
        if (!UnblockedLevels.Contains(levelIndex))
            UnblockedLevels.Add(levelIndex);
    }
}
