using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitOnFieldMenu : MonoBehaviour, IPointerExitHandler
{
    private Unit unitOnPlace;

    [SerializeField] private Transform menu;
    public static UnitOnFieldMenu Instance;

    [SerializeField] private Button sellButton;
    [SerializeField] private Button upgradeButton;

    private void Awake()
    {
        unitOnPlace = null;
        menu.gameObject.SetActive(false);
        Instance = this;
    }

    public void Open(Unit unit)
    {
        menu.gameObject.SetActive(true);
        transform.position = Input.mousePosition;
        unitOnPlace = unit;
        UpdateMenu();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        menu.gameObject.SetActive(false);
    }

    public void SellUnit()
    {
        menu.gameObject.SetActive(false);
        unitOnPlace.SellUnit();
        UpdateMenu();
    }
    private void UpdateMenu()
    {
        if (!unitOnPlace.isMaxLevel)
        {
            sellButton.transform.GetChild(0).GetComponent<Text>().text = "Продать (" + unitOnPlace.SellCost.ToString() + ")";
            upgradeButton.transform.GetChild(0).GetComponent<Text>().text = "Улучшить (" + unitOnPlace.ImprovementCost.ToString() + ")";
        }
        else
        {
            sellButton.transform.GetChild(0).GetComponent<Text>().text = "Продать (" + unitOnPlace.SellCost.ToString() + ")";
            upgradeButton.interactable = false;
            upgradeButton.transform.GetChild(0).GetComponent<Text>().text = "Максимальный \n уровень";
        }
    }
    public void UpgradeUnit()
    {
        if (!unitOnPlace.isMaxLevel)
            unitOnPlace.UpLevel();
        UpdateMenu();
    }
}
