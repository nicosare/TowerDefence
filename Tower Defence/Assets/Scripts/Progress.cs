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
                    PlayerInfo.CountUnblockedLevelsHumans++;
                    unblockedLevelsHumans[level] = true;
                    break;
                case "Humans":
                    PlayerInfo.CountUnblockedLevelsHumans++;
                    unblockedLevelsHumans[level] = true;
                    break;
                case "�����":
                    PlayerInfo.CountUnblockedLevelsElves++;
                    unblockedLevelsElves[level] = true;
                    break;
                case "Elves":
                    PlayerInfo.CountUnblockedLevelsElves++;
                    unblockedLevelsElves[level] = true;
                    break;
                case "�����":
                    PlayerInfo.CountUnblockedLevelsGnomes++;
                    unblockedLevelsGnomes[level] = true;
                    break;
                case "Dwarves":
                    PlayerInfo.CountUnblockedLevelsGnomes++;
                    unblockedLevelsGnomes[level] = true;
                    break;
                case "�������":
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
            case "����":
                return false;
            case "Humans":
                return false;
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
        Save();
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
