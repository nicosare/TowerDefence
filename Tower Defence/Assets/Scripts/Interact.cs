using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Interact : MonoBehaviour
{

    private Ray ray;
    private RaycastHit hit;
    public Unit UnitToSpawn;

    private void Update()
    {
        Ray();
        Interactive();
    }

    private void Ray()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
    }
    private void Interactive()
    {
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Place" && UnitToSpawn != null)
            {
                Setunit(UnitToSpawn);
            }
        }
    }

    private void Setunit(Unit unit)
    {
        var place = hit.transform.gameObject.GetComponent<Place>();

        if (place.isFree)
        {
            place.Preview();

            if (Input.GetMouseButtonDown(0))
            {
                unit.BuyUnit();
                place.SetUnit(unit);
                UnitToSpawn = null;
            }
        }
    }
}