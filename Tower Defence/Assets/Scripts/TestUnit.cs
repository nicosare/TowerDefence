using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TestUnit : Unit
{
    [Range(0.1f, 10)]
    [SerializeField] private int shootSpeed;
    [SerializeField] private Bullet bullet;

    protected override void Attack()
    {
        Shoot();
    }

    private void Shoot()
    {
        var newBullet = Instantiate(bullet.gameObject, transform).GetComponent<Bullet>();
        newBullet.transform.localPosition = Vector3.zero;
        newBullet.Damage = damage;
        newBullet.isPiercingAttack = isPiercingAttack;
        newBullet.target = target.transform;
        newBullet.ShootSpeed = shootSpeed;

    }
}
