using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitCell : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Unit unit;
    private Interact interact;

    public UnityEvent OnPointerDown;

    private void Awake()
    {
        interact = FindObjectOfType<Interact>();

        transform.GetChild(0).GetComponent<Text>().text = unit.NameUnit;
        transform.GetChild(1).GetComponent<Text>().text = unit.PurchaseCost.ToString();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {

        if (EconomicModel.Instance.countCoins >= unit.PurchaseCost)
            OnPointerDown?.Invoke();
        else
            Message.Instance.LoadMessage("Недостаточно денег!");
    }

    public void ChooseUnit()
    {
        interact.UnitToSpawn = unit;
    }
}
