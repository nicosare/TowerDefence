using Palmmedia.ReportGenerator.Core.Reporting.Builders.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{

    private Ray ray;
    private RaycastHit hit;
    public Unit UnitToSpawn;
    [SerializeField] private LayerMask placeLayerMask;
    [SerializeField] private LayerMask UnitLayerMask;
    [SerializeField] private LayerMask UILayerMask;
    [SerializeField] private Material previewMaterial;

    public GameObject UnitToPreview;
    public bool CanClear;
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
        {
            previewMaterial.color = new Color(1, 0, 0, .25f);
            if (CanClear && Input.GetMouseButtonDown(0))
            {
                ClearUnitToSet();
            }
            CanClear = true;
        }
    }

    public void PreviewUnit(Unit unit)
    {
        if (UnitToPreview == null)
        {
            UnitToPreview = Instantiate(unit.transform.GetChild(0).gameObject);
            Destroy(UnitToPreview.GetComponent<Collider>());
        }

        var renderers = UnitToPreview.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
            ChangeMaterial(renderer);

        UnitToPreview.transform.position = hit.point;
    }

    private void ChangeMaterial(Renderer mainModel)
    {
        var materials = new Material[mainModel.materials.Length];
        for (int i = 0; i < materials.Length; i++)
            materials[i] = previewMaterial;
        mainModel.materials = materials;
    }

    private void OpenMenu()
    {
        var unit = hit.transform.parent.GetComponent<Unit>();
        UnitOnFieldMenu.Instance.Open(unit);
        unit.transform.parent.GetComponent<Place>().Preview(unit);
    }

    private void Setunit(Unit unit)
    {
        var place = hit.transform.GetComponent<Place>();

        if (place.isFree)
        {
            previewMaterial.color = new Color(0, 1, 0, .25f);
            place.Preview(unit);

            if (Input.GetMouseButtonDown(0))
            {
                unit.BuyUnit();
                place.SetUnit(unit);
                ClearUnitToSet();
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            ClearUnitToSet();
        }
        else
            previewMaterial.color = new Color(1, 0, 0, .25f);
    }

    public void ClearUnitToSet()
    {
        UnitToSpawn = null;
        Destroy(UnitToPreview);
    }
}