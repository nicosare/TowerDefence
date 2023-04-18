using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class Yandex : MonoBehaviour
{
    public static Yandex Instance;
    [SerializeField] private Button rateGameButton;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void RemoveRateGameButton()
    {
        rateGameButton.transform.gameObject.SetActive(false);
    }

    public void DisableRateButton()
    {
        rateGameButton.enabled = false;
    }

    [DllImport("__Internal")]
    private static extern void RateGame();

    public void RateGameButton()
    {
        RateGame();
    }
}
