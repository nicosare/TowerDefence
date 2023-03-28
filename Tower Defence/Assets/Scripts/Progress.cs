using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public bool[] unblockedLevelsElves;
    public bool[] unblockedLevelsGnomes;
    public bool[] unblockedLevelsGoblins;
    public bool[] unblockedLevelsHumans;
    public bool unblockedHumans;
    public bool unblockedElves;
    public bool unblockedGnomes;
    public bool unblockedGoblins;
    public bool isComletedHumans;
    public bool isComletedElves;
    public bool isComletedGnomes;
    public bool isComletedGoblins;
}

public class Progress : MonoBehaviour
{
    public static Progress Instance;
    public PlayerInfo PlayerInfo;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        PlayerInfo.unblockedLevelsElves = new bool[15];
        PlayerInfo.unblockedLevelsGnomes = new bool[15];
        PlayerInfo.unblockedLevelsGoblins = new bool[15];
        PlayerInfo.unblockedLevelsHumans = new bool[15];

        OpenAllLevels(PlayerInfo.unblockedLevelsHumans);
        OpenAllLevels(PlayerInfo.unblockedLevelsGoblins);
        OpenAllLevels(PlayerInfo.unblockedLevelsElves);
        OpenAllLevels(PlayerInfo.unblockedLevelsGnomes);

        PlayerInfo.unblockedLevelsElves[0] = true;
        PlayerInfo.unblockedLevelsGnomes[0] = true;
        PlayerInfo.unblockedLevelsGoblins[0] = true;
        PlayerInfo.unblockedLevelsHumans[0] = true;

        PlayerInfo.unblockedHumans = true;
    }

    private void OpenAllLevels(bool[] fraction)
    {
        for (int i = 0; i < fraction.Length; i++)
            fraction[i] = true;
    }

    public bool[] GetUnblockedLevelsByNameFraction(string nameFraction)
    {
        switch (nameFraction)
        {
            case "����":
                return PlayerInfo.unblockedLevelsHumans;
            case "Humans":
                return PlayerInfo.unblockedLevelsHumans;
            case "�����":
                return PlayerInfo.unblockedLevelsElves;
            case "Elves":
                return PlayerInfo.unblockedLevelsElves;
            case "�����":
                return PlayerInfo.unblockedLevelsGnomes;
            case "Dwarves":
                return PlayerInfo.unblockedLevelsGnomes;
            case "�������":
                return PlayerInfo.unblockedLevelsGoblins;
            case "Goblins":
                return PlayerInfo.unblockedLevelsGoblins;
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
                    PlayerInfo.unblockedLevelsHumans[level] = true;
                    break;
                case "Humans":
                    PlayerInfo.unblockedLevelsHumans[level] = true;
                    break;
                case "�����":
                    PlayerInfo.unblockedLevelsElves[level] = true;
                    break;
                case "Elves":
                    PlayerInfo.unblockedLevelsElves[level] = true;
                    break;
                case "�����":
                    PlayerInfo.unblockedLevelsGnomes[level] = true;
                    break;
                case "Dwarves":
                    PlayerInfo.unblockedLevelsGnomes[level] = true;
                    break;
                case "�������":
                    PlayerInfo.unblockedLevelsGoblins[level] = true;
                    break;
                case "Goblins":
                    PlayerInfo.unblockedLevelsGoblins[level] = true;
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
                return !PlayerInfo.unblockedHumans;
            case "Humans":
                return !PlayerInfo.unblockedHumans;
            case "�����":
                return !PlayerInfo.unblockedElves;
            case "Elves":
                return !PlayerInfo.unblockedElves;
            case "�����":
                return !PlayerInfo.unblockedGnomes;
            case "Dwarves":
                return !PlayerInfo.unblockedGnomes;
            case "�������":
                return !PlayerInfo.unblockedGoblins;
            case "Goblins":
                return !PlayerInfo.unblockedGoblins;
        }
        throw new System.Exception("incorrent nameFraction");
    }

    public bool CheckIsCompletedFractionByName(string nameFraction)
    {
        switch (nameFraction)
        {
            case "����":
                return PlayerInfo.isComletedHumans;
            case "Humans":
                return PlayerInfo.isComletedHumans;
            case "�����":
                return PlayerInfo.isComletedElves;
            case "Elves":
                return PlayerInfo.isComletedElves;
            case "�����":
                return PlayerInfo.isComletedGnomes;
            case "Dwarves":
                return PlayerInfo.isComletedGnomes;
            case "�������":
                return PlayerInfo.isComletedGoblins;
            case "Goblins":
                return PlayerInfo.isComletedGoblins;
        }
        throw new System.Exception("incorrent nameFraction");
    }

    public void UnblockFractionByNameFraction(string nameFraction)
    {
        switch (nameFraction)
        {
            case "����":
                PlayerInfo.unblockedHumans = true;
                break;
            case "Humans":
                PlayerInfo.unblockedHumans = true;
                break;
            case "�����":
                PlayerInfo.unblockedElves = true;
                break;
            case "Elves":
                PlayerInfo.unblockedElves = true;
                break;
            case "�����":
                PlayerInfo.unblockedGnomes = true;
                break;
            case "Dwarves":
                PlayerInfo.unblockedGnomes = true;
                break;
            case "�������":
                PlayerInfo.unblockedGoblins = true;
                break;
            case "Goblins":
                PlayerInfo.unblockedGoblins = true;
                break;
        }
    }

    public void SetIsCompletedFractionByName(string nameFraction)
    {
        switch (nameFraction)
        {
            case "����":
                PlayerInfo.isComletedHumans = true;
                break;
            case "Humans":
                PlayerInfo.isComletedHumans = true;
                break;
            case "�����":
                PlayerInfo.isComletedElves = true;
                break;
            case "Elves":
                PlayerInfo.isComletedElves = true;
                break;
            case "�����":
                PlayerInfo.isComletedGnomes = true;
                break;
            case "Dwarves":
                PlayerInfo.isComletedGnomes = true;
                break;
            case "�������":
                PlayerInfo.isComletedGoblins = true;
                break;
            case "Goblins":
                PlayerInfo.isComletedGoblins = true;
                break;
            default:
                throw new System.Exception("incorrent nameFraction");
        }
    }
}
