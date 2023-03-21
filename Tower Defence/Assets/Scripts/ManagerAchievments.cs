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
        if(Progress.Instance.CheckIsCompletedFractionByName("����"))
        {
            achievmentHumans.SetActive(true);
        }

        if (Progress.Instance.CheckIsCompletedFractionByName("�����"))
        {
            achievmentElves.SetActive(true);
        }

        if (Progress.Instance.CheckIsCompletedFractionByName("�����"))
        {
            achievmentGnomes.SetActive(true);
        }

        if (Progress.Instance.CheckIsCompletedFractionByName("�������"))
        {
            achievmentGoblins.SetActive(true);
        }
    }
}
