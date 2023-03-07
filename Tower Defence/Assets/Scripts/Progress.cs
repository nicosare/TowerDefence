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
            case "����":
                return UnblockedLevelsHumans;
            case "Humans":
                return UnblockedLevelsHumans;
            case "�����":
                return UnblockedLevelsElves;
            case "Elves":
                return UnblockedLevelsElves;
            case "�����":
                return UnblockedLevelsGnomes;
            case "Dwarves":
                return UnblockedLevelsGnomes;
            case "�������":
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
                case "����":
                    UnblockedLevelsHumans[level] = true;
                    break;
                case "Humans":
                    UnblockedLevelsHumans[level] = true;
                    break;
                case "�����":
                    UnblockedLevelsElves[level] = true;
                    break;
                case "Elves":
                    UnblockedLevelsElves[level] = true;
                    break;
                case "�����":
                    UnblockedLevelsGnomes[level] = true;
                    break;
                case "Dwarves":
                    UnblockedLevelsGnomes[level] = true;
                    break;
                case "�������":
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
                case "����":
                    IsComletedHumans = true;
                    break;
                case "Humans":
                    IsComletedHumans = true;
                    break;
                case "�����":
                    IsComletedElves = true;
                    break;
                case "Elves":
                    IsComletedElves = true;
                    break;
                case "�����":
                    IsComletedGnomes = true;
                    break;
                case "Dwarves":
                    IsComletedGnomes = true;
                    break;
                case "�������":
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
