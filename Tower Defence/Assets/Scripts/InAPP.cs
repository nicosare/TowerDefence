using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class InAPP : MonoBehaviour
{
    [SerializeField] private GameObject chooseNextFraction;
    [DllImport("__Internal")]
    private static extern string BuyFraction();

    public void BuyFractionButton()
    {
        BuyFraction();
    }

    public void OpenChooseNextFraction()
    {
        chooseNextFraction.SetActive(true);
    }
}
