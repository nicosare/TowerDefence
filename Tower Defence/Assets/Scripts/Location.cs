using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Location", order = 3)]
public class Location : ScriptableObject
{
    public string LocationName;
    public Color BGColor;
}
