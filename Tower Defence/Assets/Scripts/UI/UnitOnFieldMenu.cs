using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnitOnFieldMenu : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    private Unit unitOnPlace;

    [SerializeField] private Transform menu;
    public static UnitOnFieldMenu Instance;

    [SerializeField] private Button sellButton;
    [SerializeField] private Button upgradeButton;
    public bool isOpened = false;
    private bool canClose = true;

    private void Awake()
    {
        menu.gameObject.SetActive(false);
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canClose)
        {
            menu.gameObject.SetActive(false);
            isOpened = false;
            canClose = false;
        }
    }

    public void Open(Unit unit)
    {
        if (!menu.gameObject.activeSelf)
        {
            menu.gameObject.SetActive(true);
            isOpened = true;
            transform.position = Input.mousePosition;
            unitOnPlace = unit;
            UpdateMenu();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canClose = true;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        canClose = false;
    }

    public void SellUnit()
    {
        unitOnPlace.SellUnit();
        menu.gameObject.SetActive(false);
        isOpened = false;
        UpdateMenu();
        if (SceneManager.GetActiveScene().name == "Level_Tutorial")
            FindObjectOfType<HowToPlayLevelManager>().NextSlideWithSell();
    }
    private void UpdateMenu()
    {
        if (LocalizationSettings.Instance.GetSelectedLocale() == LocalizationSettings.AvailableLocales.Locales[1])
        {
            if (!unitOnPlace.IsMaxLevel)
            {
                sellButton.transform.GetChild(0).GetComponent<Text>().text = "Продать\n(" + unitOnPlace.SellPrice.ToString() + ")";
                upgradeButton.interactable = true;
                upgradeButton.transform.GetChild(0).GetComponent<Text>().text = "Улучшить\n(" + unitOnPlace.UpgradePrice.ToString() + ")";
            }
            else
            {
                sellButton.transform.GetChild(0).GetComponent<Text>().text = "Продать\n(" + unitOnPlace.SellPrice.ToString() + ")";
                upgradeButton.interactable = false;
                upgradeButton.transform.GetChild(0).GetComponent<Text>().text = "Максимальный\nуровень";
            }
        }
        else
        {
            if (!unitOnPlace.IsMaxLevel)
            {
                sellButton.transform.GetChild(0).GetComponent<Text>().text = "Sell\n(" + unitOnPlace.SellPrice.ToString() + ")";
                upgradeButton.interactable = true;
                upgradeButton.transform.GetChild(0).GetComponent<Text>().text = "Up\n(" + unitOnPlace.UpgradePrice.ToString() + ")";
            }
            else
            {
                sellButton.transform.GetChild(0).GetComponent<Text>().text = "Sell\n(" + unitOnPlace.SellPrice.ToString() + ")";
                upgradeButton.interactable = false;
                upgradeButton.transform.GetChild(0).GetComponent<Text>().text = "Max\nlevel";
            }
        }
    }
    public void UpgradeUnit()
    {
        if (!unitOnPlace.IsMaxLevel)
            unitOnPlace.UpLevel();
        UpdateMenu();
        if (SceneManager.GetActiveScene().name == "Level_Tutorial")
            FindObjectOfType<HowToPlayLevelManager>().NextSlideWithUpgrade();
    }
}
