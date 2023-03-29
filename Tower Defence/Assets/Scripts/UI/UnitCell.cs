using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnitCell : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject icon;
    [SerializeField] private GameObject nameUnit;
    [SerializeField] private GameObject buyPrice;
    [SerializeField] private GameObject videoIcon;
    [SerializeField] private Slider progressBar;
    private Interact interact;
    public UnityEvent OnPointerDown;
    private Unit unit;
    private bool isCooldownBuyUnit;
    private int cooldownTime = 60;

    private void Start()
    {
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
        if (progressBar.value != progressBar.minValue)
            progressBar.value = progressBar.minValue + progressBar.maxValue - Time.time;
        else if (isCooldownBuyUnit)
            isCooldownBuyUnit = false;
    }

    public void ApplyParameters(Unit unitFromFraction)
    {
        unit = unitFromFraction;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (!isCooldownBuyUnit)
        {
            if (EconomicModel.Instance.countCoins >= unit.BuyPrice)
                OnPointerDown?.Invoke();
            else
            {
                if (LocalizationSettings.Instance.GetSelectedLocale() == LocalizationSettings.AvailableLocales.Locales[0])
                    Message.Instance.LoadMessage("Not enough money!");
                else
                    Message.Instance.LoadMessage("Недостаточно денег!");
                interact.UnitToSpawn = null;
            }
        }
    }

    public void ChooseUnit()
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
