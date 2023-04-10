using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnitCell : MonoBehaviour
{
    [SerializeField] private GameObject icon;
    [SerializeField] private GameObject nameUnit;
    [SerializeField] private GameObject buyPrice;
    [SerializeField] private GameObject videoIcon;
    [SerializeField] private Slider progressBar;
    private Interact interact;
    private Unit unit;
    private bool isCooldownBuyUnit;
    private int cooldownTime = 60;
    private Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        interact = FindObjectOfType<Interact>();
        icon.GetComponent<Image>().sprite = unit.Icon;
        nameUnit.GetComponent<Text>().text = unit.NameUnit;
        buyPrice.GetComponent<Text>().text = unit.BuyPrice.ToString();
        if (unit.BuyPrice == 0)
        {
            buyPrice.SetActive(false);
            videoIcon.SetActive(true);
        }
    }

    private void Update()
    {
        if (unit.BuyPrice <= EconomicModel.Instance.countCoins)
            button.interactable = true;
        else
            button.interactable = false;

        if (progressBar.value == progressBar.minValue && isCooldownBuyUnit)
            isCooldownBuyUnit = false;
        else if (isCooldownBuyUnit)
            progressBar.value = progressBar.minValue + progressBar.maxValue - Time.time;
    }

    public void ApplyParameters(Unit unitFromFraction)
    {
        unit = unitFromFraction;
    }

    public void ChooseUnit()
    {
        if (!isCooldownBuyUnit)
        {
            interact.UnitToSpawn = unit;
            Destroy(interact.UnitToPreview);
            interact.CanClear = false;

            if (SceneManager.GetActiveScene().name == "Level_Tutorial")
                FindObjectOfType<HowToPlayLevelManager>().NextSlideWithUnitCell();
            if (unit.BuyPrice == 0)
            {
                isCooldownBuyUnit = true;
                progressBar.minValue = Time.time;
                progressBar.maxValue = Time.time + cooldownTime;
                progressBar.value = progressBar.maxValue;
            }
        }
    }
}
