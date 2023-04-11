using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnitOnFieldMenu : MonoBehaviour, IPointerExitHandler
{
    private Unit unitOnPlace;

    [SerializeField] private Transform menu;
    public static UnitOnFieldMenu Instance;

    [SerializeField] private Button sellButton;
    [SerializeField] private Button upgradeButton;
    public bool isOpened = false;
    private DeviceType deviceType;


    private void Awake()
    {
        deviceType = SystemInfo.deviceType;
        menu.gameObject.SetActive(false);
        Instance = this;

    }

    private void Update()
    {
        if (deviceType == DeviceType.Handheld && Input.touchCount > 0)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
                MenuSetActive(false);
        }
    }

    public void Open(Unit unit)
    {
        if (!menu.gameObject.activeSelf)
        {
            MenuSetActive(true);
            transform.position = Input.mousePosition;
            unitOnPlace = unit;
            UpdateMenu();
        }
    }

    public void SellUnit()
    {
        unitOnPlace.SellUnit();
        MenuSetActive(false);
        UpdateMenu();
        if (SceneManager.GetActiveScene().name == "Level_Tutorial")
            FindObjectOfType<HowToPlayLevelManager>().NextSlideWithSell();
    }

    private void MenuSetActive(bool active)
    {
        menu.gameObject.SetActive(active);
        isOpened = active;
    }

    private void UpdateMenu()
    {
        if (LocalizationSettings.Instance.GetSelectedLocale() == LocalizationSettings.AvailableLocales.Locales[1])
        {
            if (!unitOnPlace.IsMaxLevel)
            {
                sellButton.transform.GetChild(0).GetComponent<Text>().text = "�������\n(" + unitOnPlace.SellPrice.ToString() + ")";
                upgradeButton.interactable = true;
                upgradeButton.transform.GetChild(0).GetComponent<Text>().text = "��������\n(" + unitOnPlace.UpgradePrice.ToString() + ")";
            }
            else
            {
                sellButton.transform.GetChild(0).GetComponent<Text>().text = "�������\n(" + unitOnPlace.SellPrice.ToString() + ")";
                upgradeButton.interactable = false;
                upgradeButton.transform.GetChild(0).GetComponent<Text>().text = "������������\n�������";
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

    public void OnPointerExit(PointerEventData eventData)
    {
        if (deviceType == DeviceType.Desktop)
            MenuSetActive(false);
    }
}
