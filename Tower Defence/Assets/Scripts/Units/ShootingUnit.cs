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
    [SerializeField] private Transform bulletSpawnPoint;
    private Animator animator;

    private void Awake()
    {
        if (TryGetComponent(out Animator anim))
            animator = anim;
    }

    protected override void Attack()
    {
        if (animator != null)
            animator.SetTrigger("Attack");

        var newBullet = Instantiate(bulletPrefab.gameObject).GetComponent<Bullet>();
        newBullet.transform.SetParent(transform);
        newBullet.transform.position = bulletSpawnPoint.position;
        newBullet.transform.GetChild(0).localRotation = transform.GetChild(0).rotation;
        newBullet.ApplyUnitParameters(Damage, isPiercingAttack, target.transform, bulletSpeed);

    }
}
