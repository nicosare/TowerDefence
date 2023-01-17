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

        transform.GetChild(0).GetComponent<Text>().text = unit.nameUnit;
        transform.GetChild(1).GetComponent<Text>().text = unit.damage.ToString();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        OnPointerDown?.Invoke();
    }

    public void ChooseUnit()
    {
        interact.UnitToSpawn = unit;
    }
}
