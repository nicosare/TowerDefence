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
    private Interact interact;
    public UnityEvent OnPointerDown;
    private Unit unit;

    private void Start()
    {
        interact = FindObjectOfType<Interact>();
        icon.GetComponent<Image>().sprite = unit.Icon;
        nameUnit.GetComponent<Text>().text = unit.NameUnit;
        buyPrice.GetComponent<Text>().text = unit.BuyPrice.ToString();
    }

    public void ApplyParameters(Unit unitFromFraction)
    {
        unit = unitFromFraction;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
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

    public void ChooseUnit()
    {
        interact.UnitToSpawn = unit;
        Destroy(interact.UnitToPreview);
        interact.CanClear = false;

        if (SceneManager.GetActiveScene().name == "HowToPlayLevel")
            FindObjectOfType<HowToPlayLevelManager>().NextSlideWithUnitCell();
    }
}
