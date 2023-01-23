using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StraightBullet : Bullet
{
    protected override void MoveToTarget()
    {
        var dir = target.position - transform.position;
        transform.Translate(dir.normalized * shootSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.GetChild(0).gameObject.SetActive(false);
        Hit();
    }
}
