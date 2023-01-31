using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShootingUnit : Unit
{
    [Range(1, 100)]
    [SerializeField] private int bulletSpeed;
    [SerializeField] private Bullet bulletPrefab;

    protected override void Attack()
    {
        var newBullet = Instantiate(bulletPrefab.gameObject).GetComponent<Bullet>();
        newBullet.transform.SetParent(transform);
        newBullet.transform.GetChild(0).localRotation = transform.GetChild(0).rotation;
        newBullet.ApplyUnitParameters(Damage, isPiercingAttack, target.transform, bulletSpeed);

    }

    private void LateUpdate()
    {
        Rotating();
    }

    private void Rotating()
    {
        if (target != null)
        {
            var newDir = Vector3.RotateTowards(transform.GetChild(0).forward, (target.transform.position - transform.GetChild(0).position), 1, 0);
            transform.GetChild(0).rotation = Quaternion.LookRotation(newDir);
        }
    }
}
