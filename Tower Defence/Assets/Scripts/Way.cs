using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Way : MonoBehaviour
{
    public List<Transform> WayPoints;
    private void Awake()
    {
        foreach (Transform wayPoint in transform)
            WayPoints.Add(wayPoint);
    }
}
