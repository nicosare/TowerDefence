using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
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
            Message.Instance.LoadMessage("Недостаточно денег!");
            interact.UnitToSpawn = null;
        }
    }

    public void ChooseUnit()
    {
        interact.UnitToSpawn = unit;
        Destroy(interact.UnitToPreview);
    }
}
