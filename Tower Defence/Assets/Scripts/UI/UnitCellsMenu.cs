using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UnitCellsMenu : MonoBehaviour
{
    [SerializeField] private UnitCell unitCell;

    private void Awake()
    {
        foreach (var unit in FactionsManager.Instance.ChoosenFaction.Units)
        {
            var unitFromFaction = Instantiate(unitCell, transform);
            unitFromFaction.ApplyParameters(unit);
        }
    }
}
