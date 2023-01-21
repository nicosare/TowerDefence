using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TestUnit : Unit
{
    [Range(1f, 10)]
    [SerializeField] private int shootSpeed;
    [SerializeField] private Bullet bulletPrefab;

    protected override void Attack()
    {
        var newBullet = Instantiate(bulletPrefab.gameObject).GetComponent<Bullet>();
        newBullet.transform.position = new Vector3(transform.position.x,
                                                 1f,
                                                 transform.position.z);
        newBullet.transform.SetParent(transform);
        newBullet.transform.localPosition = Vector3.zero;
        newBullet.ApplyUnitParameters(Damage, isPiercingAttack, target.transform, shootSpeed);
    }
}
