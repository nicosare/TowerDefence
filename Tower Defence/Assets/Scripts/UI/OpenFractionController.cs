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
        HumansButton.SetActive(Progress.Instance.CheckBanFractionByName("����"));
        ElvesButton.SetActive(Progress.Instance.CheckBanFractionByName("�����"));
        GnomesButton.SetActive(Progress.Instance.CheckBanFractionByName("�����"));
        GoblinsButton.SetActive(Progress.Instance.CheckBanFractionByName("�������"));
    }

    public void UnblockHumans()
    {
        Progress.Instance.UnblockFractionByNameFraction("����");
    }

    public void UnblockElves()
    {
        Progress.Instance.UnblockFractionByNameFraction("�����");
    }

    public void UnblockGnomes()
    {
        Progress.Instance.UnblockFractionByNameFraction("�����");
    }

    public void UnblockGoblins()
    {
        Progress.Instance.UnblockFractionByNameFraction("�������");
    }
}
