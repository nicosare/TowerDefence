using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public int CountUnblockedLevelsElves;
    public int CountUnblockedLevelsGnomes;
    public int CountUnblockedLevelsGoblins;
    public int CountUnblockedLevelsHumans;
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
    private bool[] unblockedLevelsElves = new bool[15];
    private bool[] unblockedLevelsGnomes = new bool[15];
    private bool[] unblockedLevelsGoblins = new bool[15];
    private bool[] unblockedLevelsHumans = new bool[15];

    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);

    [DllImport("__Internal")]
    private static extern void LoadExtern();

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
            LoadExtern();
        }
        openLevels(PlayerInfo.CountUnblockedLevelsHumans, unblockedLevelsHumans);
        openLevels(PlayerInfo.CountUnblockedLevelsElves, unblockedLevelsElves);
        openLevels(PlayerInfo.CountUnblockedLevelsGnomes, unblockedLevelsGnomes);
        openLevels(PlayerInfo.CountUnblockedLevelsGoblins, unblockedLevelsGoblins);
    }

    private void openLevels(int countLevels, bool[] fractionLevels)
    {
        for (int i = 0; i <= countLevels; i++)
            fractionLevels[i] = true;
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
                    PlayerInfo.CountUnblockedLevelsHumans++;
                    unblockedLevelsHumans[level] = true;
                    break;
                case "Humans":
                    PlayerInfo.CountUnblockedLevelsHumans++;
                    unblockedLevelsHumans[level] = true;
                    break;
                case "Эльфы":
                    PlayerInfo.CountUnblockedLevelsElves++;
                    unblockedLevelsElves[level] = true;
                    break;
                case "Elves":
                    PlayerInfo.CountUnblockedLevelsElves++;
                    unblockedLevelsElves[level] = true;
                    break;
                case "Гномы":
                    PlayerInfo.CountUnblockedLevelsGnomes++;
                    unblockedLevelsGnomes[level] = true;
                    break;
                case "Dwarves":
                    PlayerInfo.CountUnblockedLevelsGnomes++;
                    unblockedLevelsGnomes[level] = true;
                    break;
                case "Гоблины":
                    PlayerInfo.CountUnblockedLevelsGoblins++;
                    unblockedLevelsGoblins[level] = true;
                    break;
                case "Goblins":
                    PlayerInfo.CountUnblockedLevelsGoblins++;
                    unblockedLevelsGoblins[level] = true;
                    break;
                default:
                    throw new System.Exception("incorrent nameFraction");
            }
        }
        Save();
    }

    public bool CheckBanFractionByName(string nameFraction)
    {
        switch (nameFraction)
        {
            case "Люди":
                return false;
            case "Humans":
                return false;
            case "Эльфы":
                return !PlayerInfo.unblockedElves;
            case "Elves":
                return !PlayerInfo.unblockedElves;
            case "Гномы":
                return !PlayerInfo.unblockedGnomes;
            case "Dwarves":
                return !PlayerInfo.unblockedGnomes;
            case "Гоблины":
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
            case "Люди":
                return PlayerInfo.isComletedHumans;
            case "Humans":
                return PlayerInfo.isComletedHumans;
            case "Эльфы":
                return PlayerInfo.isComletedElves;
            case "Elves":
                return PlayerInfo.isComletedElves;
            case "Гномы":
                return PlayerInfo.isComletedGnomes;
            case "Dwarves":
                return PlayerInfo.isComletedGnomes;
            case "Гоблины":
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
            case "Эльфы":
                PlayerInfo.unblockedElves = true;
                break;
            case "Elves":
                PlayerInfo.unblockedElves = true;
                break;
            case "Гномы":
                PlayerInfo.unblockedGnomes = true;
                break;
            case "Dwarves":
                PlayerInfo.unblockedGnomes = true;
                break;
            case "Гоблины":
                PlayerInfo.unblockedGoblins = true;
                break;
            case "Goblins":
                PlayerInfo.unblockedGoblins = true;
                break;
        }
        Save();
    }

    public void SetIsCompletedFractionByName(string nameFraction)
    {
        switch (nameFraction)
        {
            case "Люди":
                PlayerInfo.isComletedHumans = true;
                break;
            case "Humans":
                PlayerInfo.isComletedHumans = true;
                break;
            case "Эльфы":
                PlayerInfo.isComletedElves = true;
                break;
            case "Elves":
                PlayerInfo.isComletedElves = true;
                break;
            case "Гномы":
                PlayerInfo.isComletedGnomes = true;
                break;
            case "Dwarves":
                PlayerInfo.isComletedGnomes = true;
                break;
            case "Гоблины":
                PlayerInfo.isComletedGoblins = true;
                break;
            case "Goblins":
                PlayerInfo.isComletedGoblins = true;
                break;
            default:
                throw new System.Exception("incorrent nameFraction");
        }
        Save();
    }

    public void Save()
    {
        string jsonPlayerInfo = JsonUtility.ToJson(PlayerInfo);
        SaveExtern(jsonPlayerInfo);
    }

    public void SetPlayerInfo(string data)
    {
        PlayerInfo = JsonUtility.FromJson<PlayerInfo>(data);
    }
}
