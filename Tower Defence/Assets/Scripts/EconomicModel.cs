using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EconomicModel : MonoBehaviour
{
    public static EconomicModel Instance;
    public int countCoins { get; private set; }
    private Text textCountCoin;

    private void Start()
    {
        countCoins = 10;
        Instance = this;
        textCountCoin = GameObject.FindGameObjectWithTag("textCountCoint").GetComponent<Text>();
        textCountCoin.text = countCoins.ToString();
    }

    public void IncreaseÑountCoin(int coins)
    {
        countCoins += coins;
        textCountCoin.text = countCoins.ToString();
    }

    public void ReduceÑountCoin(int coints)
    {
        countCoins -= coints;
        textCountCoin.text = countCoins.ToString();
    }
}
