using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class InAPP : MonoBehaviour
{
    public static InAPP Instance;
    [SerializeField] private GameObject chooseNextFraction;
    [SerializeField] private OpenFractionController openFractionController;
    [SerializeField] private ButtonsManager buttonsManager;
    private string nameFractionForBuy;
    [DllImport("__Internal")]
    private static extern string BuyFraction();

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

    public void BuyFractionButton(string nameFraction)
    {
        nameFractionForBuy = nameFraction;
        BuyFraction();
    }

    public void OpenFractionSuccessful()
    {
        Progress.Instance.UnblockFractionByNameFraction(nameFractionForBuy);
        //switch (nameFraction)
        //{
        //    case "����":
        //        openFractionController.UnblockHumans();
        //        break;
        //    case "�����":
        //        openFractionController.UnblockElves();
        //        break;
        //    case "�����":
        //        openFractionController.UnblockGnomes();
        //        break;
        //    case "�������":
        //        openFractionController.UnblockGoblins();
        //        break;
        //}
        chooseNextFraction.SetActive(false);
        buttonsManager.GoToScene("SelectFactionMenu");
    }

    public void OpenFractionUnsuccessful()
    {
        chooseNextFraction.SetActive(false);
    }
}
