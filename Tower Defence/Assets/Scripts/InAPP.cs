using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class InAPP : MonoBehaviour
{
    [SerializeField] private GameObject chooseNextFraction;
    [SerializeField] private OpenFractionController openFractionController;
    [DllImport("__Internal")]
    private static extern string BuyFraction(string nameFraction);

    public void BuyFractionButton(string nameFraction)
    {
        BuyFraction(nameFraction);
    }

    public void OpenFractionSuccessful(string nameFraction)
    {
        switch (nameFraction)
        {
            case "����":
                openFractionController.UnblockHumans();
                break;
            case "�����":
                openFractionController.UnblockElves();
                break;
            case "�����":
                openFractionController.UnblockGnomes();
                break;
            case "�������":
                openFractionController.UnblockGoblins();
                break;
        }
        chooseNextFraction.SetActive(false);
    }

    public void OpenFractionUnsuccessful()
    {
        chooseNextFraction.SetActive(false);
    }
}
