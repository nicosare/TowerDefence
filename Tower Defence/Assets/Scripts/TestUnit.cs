using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TestUnit : Unit
{
    [Range(1f, 10)]
    [SerializeField] private int shootSpeed;
    [SerializeField] private Bullet bullet;

    protected override void Attack()
    {
        var newBullet = Instantiate(bullet.gameObject).GetComponent<Bullet>();
        newBullet.transform.position = new Vector3(transform.position.x,
                                                 1f,
                                                 transform.position.z);
        newBullet.transform.SetParent(transform);
        newBullet.transform.localPosition = Vector3.zero;
        newBullet.Damage = damage;
        newBullet.isPiercingAttack = isPiercingAttack;
        newBullet.target = target.transform;
        newBullet.ShootSpeed = shootSpeed;
    }
}
