using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class InAPP : MonoBehaviour
{
    [SerializeField] private GameObject chooseNextFraction;
    [SerializeField] private GameObject infoAndBuyFractionWindow;
    [DllImport("__Internal")]
    private static extern string BuyFraction();

    public void BuyFractionButton()
    {
        BuyFraction();
    }

    public void OpenChooseNextFraction()
    {
        infoAndBuyFractionWindow.SetActive(false);
        chooseNextFraction.SetActive(true);
    }
}
