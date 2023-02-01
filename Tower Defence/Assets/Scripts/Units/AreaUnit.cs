using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AreaUnit : Unit
{
    [Range(1, 10)]
    [SerializeField] private int reloadTime;
    [Range(1, 100)]
    [SerializeField] private int bulletSpeed;
    [SerializeField] private int bulletCount;
    [SerializeField] private Bullet bulletPrefab;
    private bool canShoot = true;
    protected override void Attack()
    {
        if (canShoot)
            StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        canShoot = false;
        for (int i = 0; i < bulletCount; i++)
        {
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
            newBullet.transform.position = new Vector3(transform.position.x,
                                                     1f,
                                                     transform.position.z);
            newBullet.transform.SetParent(transform);
            newBullet.transform.localPosition = Vector3.zero;
            newBullet.ApplyUnitParameters(Damage, isPiercingAttack, target.transform, bulletSpeed);
        }
    }
}

