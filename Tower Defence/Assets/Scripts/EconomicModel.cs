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
        countCoins = 100;
        Instance = this;
        textCountCoin = GameObject.FindGameObjectWithTag("textCountCoint").GetComponent<Text>();
        textCountCoin.text = countCoins.ToString();
    }

    public void IncreaseCountCoin(int coins)
    {
        StartCoroutine(Counting(coins, true));
    }

    IEnumerator Counting(int coins, bool isIncrease)
    {
        for (; coins > 0; coins--)
        {
            if (isIncrease)
            {
                countCoins += 1;
                textCountCoin.text = countCoins.ToString();
            }
            else
            {
                countCoins -= 1;
                textCountCoin.text = countCoins.ToString();
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    public void Reduce—ountCoin(int coins)
    {
        StartCoroutine(Counting(coins, false));
    }
}
