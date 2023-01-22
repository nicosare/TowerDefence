using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Interact : MonoBehaviour
{

    private Ray ray;
    private RaycastHit hit;
    public Unit UnitToSpawn;
    [SerializeField] private LayerMask placeLayerMask;
    [SerializeField] private LayerMask UnitLayerMask;

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
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placeLayerMask))
        {
            if (UnitToSpawn != null)
            {
                if (hit.collider.gameObject.tag == "Place" && !UnitToSpawn.IsRoadUnit)
                    Setunit(UnitToSpawn);

                if (hit.collider.gameObject.tag == "Road" && UnitToSpawn.IsRoadUnit)
                    Setunit(UnitToSpawn);
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, UnitLayerMask))
            {

                if (Input.GetMouseButtonDown(0))
                {
                    UnitToSpawn = null;
                    OpenMenu();
                }
            }
        }
    }

    private void OpenMenu()
    {
        var unit = hit.transform.parent.GetComponent<Unit>();
        UnitOnFieldMenu.Instance.Open(unit);
    }

    private void Setunit(Unit unit)
    {
        var place = hit.transform.GetComponent<Place>();

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
        else if (Input.GetMouseButtonDown(0))
        {
            Message.Instance.LoadMessage("Место занято!");
            UnitToSpawn = null;
        }
    }
}