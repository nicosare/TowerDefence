using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Place : MonoBehaviour
{
    public bool isFree;
    private Transform glow;
    private Transform spawnPoint;
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
        newUnit.transform.position = new Vector3(spawnPoint.position.x,
                                                 spawnPoint.position.y + newUnit.transform.GetChild(0).localScale.y,
                                                 spawnPoint.position.z);
        newUnit.transform.SetParent(transform);
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
