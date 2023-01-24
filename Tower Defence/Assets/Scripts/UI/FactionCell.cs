using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FactionCell : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Faction faction;
    public UnityEvent OnPointerDown;

    private void Awake()
    {

        transform.GetComponent<Image>().sprite = faction.IconFaction;
        transform.GetChild(0).GetComponent<Text>().text = faction.NameFaction;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        OnPointerDown?.Invoke();
    }

    public void ChooseFaction()
    {
        FactionsManager.Instance.ChoosenFaction = faction;
    }
}
