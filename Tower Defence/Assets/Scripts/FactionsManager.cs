using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionsManager : MonoBehaviour
{
    public Faction ChoosenFaction;

    public static FactionsManager Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}