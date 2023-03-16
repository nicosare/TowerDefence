using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAchievments : MonoBehaviour
{
    [SerializeField] private GameObject achievmentHumans;
    [SerializeField] private GameObject achievmentElves;
    [SerializeField] private GameObject achievmentGnomes;
    [SerializeField] private GameObject achievmentGoblins;
    void Start()
    {
        if(Progress.Instance.CheckIsCompletedFractionByName("Люди"))
        {
            achievmentHumans.SetActive(true);
        }

        if (Progress.Instance.CheckIsCompletedFractionByName("Эльфы"))
        {
            achievmentElves.SetActive(true);
        }

        if (Progress.Instance.CheckIsCompletedFractionByName("Гномы"))
        {
            achievmentGnomes.SetActive(true);
        }

        if (Progress.Instance.CheckIsCompletedFractionByName("Гоблины"))
        {
            achievmentGoblins.SetActive(true);
        }
    }
}
