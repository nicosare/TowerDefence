using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    public static Progress Instance;
    private bool[] unblockedLevelsElves;
    private bool[] unblockedLevelsGnomes;
    private bool[] unblockedLevelsGoblins;
    private bool[] unblockedLevelsHumans;
    private bool unblockedHumans = true;
    private bool unblockedElves;
    private bool unblockedGnomes;
    private bool unblockedGoblins;
    private bool isComletedHumans;
    private bool isComletedElves;
    private bool isComletedGnomes;
    private bool isComletedGoblins;


    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        unblockedLevelsElves = new bool[15];
        unblockedLevelsGnomes = new bool[15];
        unblockedLevelsGoblins = new bool[15];
        unblockedLevelsHumans = new bool[15];

        unblockedLevelsElves[0] = true;
        unblockedLevelsGnomes[0] = true;
        unblockedLevelsGoblins[0] = true;
        unblockedLevelsHumans[0] = true;
    }


    public bool[] GetUnblockedLevelsByNameFraction(string nameFraction)
    {
        switch (nameFraction)
        {
            case "Люди":
                return unblockedLevelsHumans;
            case "Humans":
                return unblockedLevelsHumans;
            case "Эльфы":
                return unblockedLevelsElves;
            case "Elves":
                return unblockedLevelsElves;
            case "Гномы":
                return unblockedLevelsGnomes;
            case "Dwarves":
                return unblockedLevelsGnomes;
            case "Гоблины":
                return unblockedLevelsGoblins;
            case "Goblins":
                return unblockedLevelsGoblins;
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
                    unblockedLevelsHumans[level] = true;
                    break;
                case "Humans":
                    unblockedLevelsHumans[level] = true;
                    break;
                case "Эльфы":
                    unblockedLevelsElves[level] = true;
                    break;
                case "Elves":
                    unblockedLevelsElves[level] = true;
                    break;
                case "Гномы":
                    unblockedLevelsGnomes[level] = true;
                    break;
                case "Dwarves":
                    unblockedLevelsGnomes[level] = true;
                    break;
                case "Гоблины":
                    unblockedLevelsGoblins[level] = true;
                    break;
                case "Goblins":
                    unblockedLevelsGoblins[level] = true;
                    break;
                default:
                    throw new System.Exception("incorrent nameFraction");
            }
        }
    }

    public bool CheckBanFractionByName(string nameFraction)
    {
        switch (nameFraction)
        {
            case "Люди":
                return !unblockedHumans;
            case "Humans":
                return !unblockedHumans;
            case "Эльфы":
                return !unblockedElves;
            case "Elves":
                return !unblockedElves;
            case "Гномы":
                return !unblockedGnomes;
            case "Dwarves":
                return !unblockedGnomes;
            case "Гоблины":
                return !unblockedGoblins;
            case "Goblins":
                return !unblockedGoblins;
        }
        throw new System.Exception("incorrent nameFraction");
    }

    public bool CheckIsCompletedFractionByName(string nameFraction)
    {
        switch (nameFraction)
        {
            case "Люди":
                return isComletedHumans;
            case "Humans":
                return isComletedHumans;
            case "Эльфы":
                return isComletedElves;
            case "Elves":
                return isComletedElves;
            case "Гномы":
                return isComletedGnomes;
            case "Dwarves":
                return isComletedGnomes;
            case "Гоблины":
                return isComletedGoblins;
            case "Goblins":
                return isComletedGoblins;
        }
        throw new System.Exception("incorrent nameFraction");
    }

    public void UnblockFractionByNameFraction(string nameFraction)
    {
        switch (nameFraction)
        {
            case "Люди":
                unblockedHumans = true;
                break;
            case "Humans":
                unblockedHumans = true;
                break;
            case "Эльфы":
                unblockedElves = true;
                break;
            case "Elves":
                unblockedElves = true;
                break;
            case "Гномы":
                unblockedGnomes = true;
                break;
            case "Dwarves":
                unblockedGnomes = true;
                break;
            case "Гоблины":
                unblockedGoblins = true;
                break;
            case "Goblins":
                unblockedGoblins = true;
                break;
        }
    }

    public void SetIsCompletedFractionByName(string nameFraction)
    {
        switch (nameFraction)
        {
            case "Люди":
                isComletedHumans = true;
                break;
            case "Humans":
                isComletedHumans = true;
                break;
            case "Эльфы":
                isComletedElves = true;
                break;
            case "Elves":
                isComletedElves = true;
                break;
            case "Гномы":
                isComletedGnomes = true;
                break;
            case "Dwarves":
                isComletedGnomes = true;
                break;
            case "Гоблины":
                isComletedGoblins = true;
                break;
            case "Goblins":
                isComletedGoblins = true;
                break;
            default:
                throw new System.Exception("incorrent nameFraction");
        }
    }
}
