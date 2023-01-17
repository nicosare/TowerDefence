using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EconomicModel : MonoBehaviour
{
    public static EconomicModel instance;
    private int countCoins = 10;
    private Text textCountCoint;

    private void Start()
    {
        instance = this;
        textCountCoint = GameObject.FindGameObjectWithTag("textCountCoint").GetComponent<Text>();
        textCountCoint.text = countCoins.ToString();
    }

    public void Increase—ountCoint(int coints)
    {
        countCoins += coints;
        textCountCoint.text = countCoins.ToString();
    }

    public void Reduce—ountCoint(int coints)
    {
        countCoins -= coints;
        textCountCoint.text = countCoins.ToString();
    }
}
