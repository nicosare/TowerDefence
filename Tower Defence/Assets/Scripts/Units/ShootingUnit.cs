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

    private Vector3 bulletSpawnPoint;

    private void Awake()
    {
        bulletSpawnPoint = transform.GetChild(2).position;
    }

    protected override void Attack()
    {

        var newBullet = Instantiate(bulletPrefab.gameObject).GetComponent<Bullet>();
        newBullet.transform.SetParent(transform);
        newBullet.transform.localPosition = bulletSpawnPoint;
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
            var newDir = new Vector3(target.transform.position.x, transform.GetChild(0).position.y, target.transform.position.z);
            transform.GetChild(0).LookAt(newDir);
        }
    }
}
