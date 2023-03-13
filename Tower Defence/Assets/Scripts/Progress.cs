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
            case "����":
                return unblockedLevelsHumans;
            case "Humans":
                return unblockedLevelsHumans;
            case "�����":
                return unblockedLevelsElves;
            case "Elves":
                return unblockedLevelsElves;
            case "�����":
                return unblockedLevelsGnomes;
            case "Dwarves":
                return unblockedLevelsGnomes;
            case "�������":
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
                case "����":
                    unblockedLevelsHumans[level] = true;
                    break;
                case "Humans":
                    unblockedLevelsHumans[level] = true;
                    break;
                case "�����":
                    unblockedLevelsElves[level] = true;
                    break;
                case "Elves":
                    unblockedLevelsElves[level] = true;
                    break;
                case "�����":
                    unblockedLevelsGnomes[level] = true;
                    break;
                case "Dwarves":
                    unblockedLevelsGnomes[level] = true;
                    break;
                case "�������":
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
            case "����":
                return !unblockedHumans;
            case "Humans":
                return !unblockedHumans;
            case "�����":
                return !unblockedElves;
            case "Elves":
                return !unblockedElves;
            case "�����":
                return !unblockedGnomes;
            case "Dwarves":
                return !unblockedGnomes;
            case "�������":
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
            case "����":
                return isComletedHumans;
            case "Humans":
                return isComletedHumans;
            case "�����":
                return isComletedElves;
            case "Elves":
                return isComletedElves;
            case "�����":
                return isComletedGnomes;
            case "Dwarves":
                return isComletedGnomes;
            case "�������":
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
            case "����":
                unblockedHumans = true;
                break;
            case "Humans":
                unblockedHumans = true;
                break;
            case "�����":
                unblockedElves = true;
                break;
            case "Elves":
                unblockedElves = true;
                break;
            case "�����":
                unblockedGnomes = true;
                break;
            case "Dwarves":
                unblockedGnomes = true;
                break;
            case "�������":
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
            case "����":
                isComletedHumans = true;
                break;
            case "Humans":
                isComletedHumans = true;
                break;
            case "�����":
                isComletedElves = true;
                break;
            case "Elves":
                isComletedElves = true;
                break;
            case "�����":
                isComletedGnomes = true;
                break;
            case "Dwarves":
                isComletedGnomes = true;
                break;
            case "�������":
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
