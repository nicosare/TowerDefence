using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", order = 1)]
public class Wave : ScriptableObject
{
    public List<Enemy> Enemies;
    public float TimeBetweenSpawn;
}
