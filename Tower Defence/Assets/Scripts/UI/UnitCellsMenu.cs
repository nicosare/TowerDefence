using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitCellsMenu : MonoBehaviour
{
    [SerializeField] private UnitCell unitCell;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name != "HowToPlayLevel")
            SetCells();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "HowToPlayLevel")
            SetCells();
    }

    private void SetCells()
    {
        foreach (var unit in FactionsManager.Instance.ChoosenFaction.Units)
        {
            var unitFromFaction = Instantiate(unitCell, transform);
            unitFromFaction.ApplyParameters(unit);
        }
    }
}
