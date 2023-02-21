using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Place : MonoBehaviour
{
    public bool isFree;
    private Transform glow;
    private Transform spawnPoint;
    public Unit UnitOnPlace;

    private void Awake()
    {
        isFree = true;
        glow = transform.GetChild(0);
        glow.gameObject.SetActive(false);
        spawnPoint = transform.GetChild(1).transform;
    }

    public void SetUnit(Unit unit)
    {
        var newUnit = Instantiate(unit.gameObject);
        
        newUnit.transform.SetParent(transform);
        newUnit.transform.position = spawnPoint.position + Vector3.up * 0.5f;
        UnitOnPlace = newUnit.GetComponent<Unit>();
        UnitOnPlace.PlaySoundInstallation();
        isFree = false;
    }

    public void Preview()
    {
        glow.gameObject.SetActive(true);
    }


    private void Update()
    {
        glow.gameObject.SetActive(false);
    }
}
