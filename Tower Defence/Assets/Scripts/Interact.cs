using Palmmedia.ReportGenerator.Core.Reporting.Builders.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Interact : MonoBehaviour
{

    private Ray ray;
    private RaycastHit hit;
    public Unit UnitToSpawn;
    [SerializeField] private LayerMask placeLayerMask;
    [SerializeField] private LayerMask UnitLayerMask;
    [SerializeField] private Material previewMaterial;

    public GameObject UnitToPreview;

    private void Update()
    {
        Ray();
        Interactive();
        if (UnitToSpawn != null)
            PreviewUnit(UnitToSpawn);
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
                if (!UnitToSpawn.IsRoadUnit)
                    SetPreviewColor("Place");
                else
                    SetPreviewColor("Road");
            }

            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, UnitLayerMask))
            {

                if (Input.GetMouseButtonDown(0))
                {
                    UnitToSpawn = null;
                    Destroy(UnitToPreview);
                    OpenMenu();
                }
            }
        }
    }

    private void SetPreviewColor(string placeTag)
    {
        if (hit.collider.gameObject.tag == placeTag)
        {
            previewMaterial.color = new Color(0, 1, 0, .25f);
            Setunit(UnitToSpawn);
        }
        else
            previewMaterial.color = new Color(1, 0, 0, .25f);
    }

    public void PreviewUnit(Unit unit)
    {
        if (UnitToPreview == null)
        {
            UnitToPreview = Instantiate(unit.transform.GetChild(0).gameObject);
            Destroy(UnitToPreview.GetComponent<Collider>());
        }

        if (UnitToPreview.TryGetComponent(out Renderer renderer))
            renderer.material = previewMaterial;
        else
            foreach (Transform model in UnitToPreview.transform)
            {
                var mats = new Material[model.GetComponent<Renderer>().materials.Length];
                for (var i = 0; i < mats.Length; i++)
                {
                    mats[i] = previewMaterial;
                }
                model.GetComponent<Renderer>().materials = mats;
            }

        UnitToPreview.transform.position = hit.point;
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
            previewMaterial.color = new Color(0, 1, 0, .25f);
            //place.Preview();

            if (Input.GetMouseButtonDown(0))
            {
                unit.BuyUnit();
                place.SetUnit(unit);
                UnitToSpawn = null;
                Destroy(UnitToPreview);
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Message.Instance.LoadMessage("Место занято!");
            UnitToSpawn = null;
            Destroy(UnitToPreview);
        }
        else
            previewMaterial.color = new Color(1, 0, 0, .25f);
    }
}