using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StraightBullet : Bullet
{
    protected override void MoveToTarget()
    {
        var dir = target.position - transform.position;
        transform.Translate(dir.normalized * ShootSpeed * Time.deltaTime);
    }
    protected override void Hit()
    {
        var damagedEnemies = Physics.OverlapBox(transform.position, transform.lossyScale / 2);

        foreach (var damagedEnemy in damagedEnemies)
            if (damagedEnemy.tag == "Enemy")
            {
                Debug.Log("Hit");
                damagedEnemy.gameObject.GetComponent<Enemy>().GetDamage(Damage, isPiercingAttack);
            }
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.lossyScale);
    }
}
