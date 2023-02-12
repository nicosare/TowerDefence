using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AreaUnit : Unit
{
    [SerializeField] private bool oneTarget;
    [Range(1, 100)]
    [SerializeField] private int bulletSpeed;
    [SerializeField] private int bulletCount;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    private bool canShoot = true;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    protected override void Attack()
    {
        animator.SetBool("Attack", canShoot);
    }

    public void StartShooting()
    {
        if (target != null)
            StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        canShoot = false;
        for (int i = 0; i < bulletCount; i++)
        {
            if (oneTarget)
                Shoot(target);
            else
                foreach (var target in targets)
                    Shoot(target);
            yield return new WaitForSeconds(1 / attackSpeed);
        }
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
    }

    private void Shoot(Enemy target)
    {
        if (target != null)
        {
            var newBullet = Instantiate(bulletPrefab.gameObject).GetComponent<Bullet>();
            newBullet.transform.SetParent(transform);
            newBullet.transform.position = bulletSpawnPoint.position;
            newBullet.transform.GetChild(0).localRotation = transform.GetChild(0).rotation;
            newBullet.ApplyUnitParameters(Damage, isPiercingAttack, target.transform, bulletSpeed);
        }
    }
}

