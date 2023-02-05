using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StraightBullet : Bullet
{
    protected override void MoveToTarget()
    {
        var moveDir = target.position - transform.position;
        transform.Translate(moveDir.normalized * shootSpeed * Time.deltaTime);

        var rotateDir = Vector3.RotateTowards(transform.GetChild(0).forward, (target.transform.position - transform.GetChild(0).position), 1, 0);
        transform.GetChild(0).rotation = Quaternion.LookRotation(rotateDir);
    }

    private void Start()
    {
        transform.localPosition = new Vector3(0, -0.705f, 0.115f);
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        Hit(other);
    }

    protected override void Hit(Collider other)
    {
        if (isHitting)
            StartCoroutine(Hitting(other));
    }

    protected override void Hit()
    {
        throw new System.NotImplementedException();
    }
}
