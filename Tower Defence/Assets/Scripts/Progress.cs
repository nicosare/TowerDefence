using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    public static Progress Instance;
    public bool[] UnblockedLevelsElves;
    public bool[] UnblockedLevelsGnomes;
    public bool[] UnblockedLevelsGoblins;
    public bool[] UnblockedLevelsHumans;
    public bool UnblockedHumans = true;
    public bool UnblockedElves;
    public bool UnblockedGnomes;
    public bool UnblockedGoblins;
    public bool IsComletedHumans;
    public bool IsComletedElves;
    public bool IsComletedGnomes;
    public bool IsComletedGoblins;


    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        UnblockedLevelsElves = new bool[15];
        UnblockedLevelsGnomes = new bool[15];
        UnblockedLevelsGoblins = new bool[15];
        UnblockedLevelsHumans = new bool[15];

        UnblockedLevelsElves[0] = true;
        UnblockedLevelsGnomes[0] = true;
        UnblockedLevelsGoblins[0] = true;
        UnblockedLevelsHumans[0] = true;
    }


    public bool[] GetUnblockedLevelsByNameFraction(string nameFraction)
    {
        switch (nameFraction)
        {
            case "Люди":
                return UnblockedLevelsHumans;
            case "Humans":
                return UnblockedLevelsHumans;
            case "Эльфы":
                return UnblockedLevelsElves;
            case "Elves":
                return UnblockedLevelsElves;
            case "Гномы":
                return UnblockedLevelsGnomes;
            case "Dwarves":
                return UnblockedLevelsGnomes;
            case "Гоблины":
                return UnblockedLevelsGoblins;
            case "Goblins":
                return UnblockedLevelsGoblins;
        }
        throw new System.Exception("incorrent nameFraction");
    }

    public void UnblockLevelByNameFraction(string nameFraction, int level)
    {
        if (level < 15)
        {
            switch (nameFraction)
            {
                case "Люди":
                    UnblockedLevelsHumans[level] = true;
                    break;
                case "Humans":
                    UnblockedLevelsHumans[level] = true;
                    break;
                case "Эльфы":
                    UnblockedLevelsElves[level] = true;
                    break;
                case "Elves":
                    UnblockedLevelsElves[level] = true;
                    break;
                case "Гномы":
                    UnblockedLevelsGnomes[level] = true;
                    break;
                case "Dwarves":
                    UnblockedLevelsGnomes[level] = true;
                    break;
                case "Гоблины":
                    UnblockedLevelsGoblins[level] = true;
                    break;
                case "Goblins":
                    UnblockedLevelsGoblins[level] = true;
                    break;
                default:
                    throw new System.Exception("incorrent nameFraction");
            }
        }
        else
        {
            switch (nameFraction)
            {
                case "Люди":
                    IsComletedHumans = true;
                    break;
                case "Humans":
                    IsComletedHumans = true;
                    break;
                case "Эльфы":
                    IsComletedElves = true;
                    break;
                case "Elves":
                    IsComletedElves = true;
                    break;
                case "Гномы":
                    IsComletedGnomes = true;
                    break;
                case "Dwarves":
                    IsComletedGnomes = true;
                    break;
                case "Гоблины":
                    IsComletedGoblins = true;
                    break;
                case "Goblins":
                    IsComletedGoblins = true;
                    break;
                default:
                    throw new System.Exception("incorrent nameFraction");
            }
        }
    }
}
