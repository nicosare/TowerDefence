using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenFractionController : MonoBehaviour
{
    [SerializeField] private GameObject HumansButton;
    [SerializeField] private GameObject ElvesButton;
    [SerializeField] private GameObject GnomesButton;
    [SerializeField] private GameObject GoblinsButton;

    void Start()
    {
        HumansButton.SetActive(Progress.Instance.CheckBanFractionByName("Люди"));
        ElvesButton.SetActive(Progress.Instance.CheckBanFractionByName("Эльфы"));
        GnomesButton.SetActive(Progress.Instance.CheckBanFractionByName("Гномы"));
        GoblinsButton.SetActive(Progress.Instance.CheckBanFractionByName("Гоблины"));
    }

    public void UnblockHumans()
    {
        Progress.Instance.UnblockFractionByNameFraction("Люди");
    }

    public void UnblockElves()
    {
        Progress.Instance.UnblockFractionByNameFraction("Эльфы");
    }

    public void UnblockGnomes()
    {
        Progress.Instance.UnblockFractionByNameFraction("Гномы");
    }

    public void UnblockGoblins()
    {
        Progress.Instance.UnblockFractionByNameFraction("Гоблины");
    }
}
