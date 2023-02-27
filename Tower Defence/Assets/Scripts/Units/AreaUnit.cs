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
    [SerializeField] private AudioClip soundAttack;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    protected override void Attack()
    {
        if (target != null && canShoot)
            animator.SetBool("Attack", true);
    }

    public void StartShooting()
    {
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        if(!gameObject.name.Contains("Elite Elf-Archer"))
            audioSource.PlayOneShot(soundAttack);
        canShoot = false;
        if (bulletCount > 1)
            animator.speed = 0;
        for (int i = 0; i < bulletCount; i++)
        {
            if (oneTarget)
                Shoot(target);
            else
                foreach (var target in targets)
                    Shoot(target);
            yield return new WaitForSeconds(1 / AttackSpeed);
        }
        animator.speed = 1;
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
    }

    private void Shoot(Enemy target)
    {
        if (target != null)
        {
            if (gameObject.name.Contains("Elite Elf-Archer"))
                audioSource.PlayOneShot(soundAttack);
            var newBullet = Instantiate(bulletPrefab.gameObject).GetComponent<Bullet>();
            newBullet.transform.SetParent(transform);
            newBullet.transform.position = bulletSpawnPoint.position;
            newBullet.transform.GetChild(0).localRotation = transform.GetChild(0).rotation;
            newBullet.ApplyUnitParameters(Damage, isPiercingAttack, target.transform, bulletSpeed);
        }
    }
}

