using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Place : MonoBehaviour, IPointerExitHandler
{
    public bool isFree;
    private Transform glow;
    private void Awake()
    {
        isFree = true;
        glow = transform.GetChild(0);
        glow.gameObject.SetActive(false);
    }
    public void SetUnit(Unit unit)
    {
        var newUnit = Instantiate(unit.gameObject);
        newUnit.transform.position = new Vector3(transform.position.x, .8f, transform.position.z);
        isFree = false;
    }

    public void Preview()
    {
        glow.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        glow.gameObject.SetActive(false);
    }
}
