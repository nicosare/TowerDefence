using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallisticBullet : Bullet
{
    private Vector3 middlePoint;
    private Vector3 point1;
    private Vector3 point2;
    private float step;

    private Vector3 targetPosition;

    private void Start()
    {
        step = shootSpeed * 0.01f;
        targetPosition = target.position;
        middlePoint = Vector3.Lerp(transform.parent.position, targetPosition, .5f) + 2 * Vector3.up.normalized;
        point1 = transform.parent.position;
        point2 = middlePoint;
        transform.position = point1;
    }
    protected override void MoveToTarget()
    {
        point1 = Vector3.MoveTowards(point1, middlePoint, step);
        point2 = Vector3.MoveTowards(point2, targetPosition, step);
        transform.position = Vector3.MoveTowards(transform.position, point2, step);

        if (transform.position == targetPosition)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            Hit();
        }
    }
}
