using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public int Damage;
    public bool isPiercingAttack;
    public Transform target;
    public float ShootSpeed;
    [Range(1, 10)]
    [SerializeField] protected float RadiusAttack;
    protected abstract void MoveToTarget();

    private void Update()
    {
        if (target != null)
            MoveToTarget();
        else
            Destroy(gameObject);
    }

    protected void Hit()
    {
        var damagedEnemies = Physics.OverlapBox(transform.position, transform.lossyScale * RadiusAttack / 2)
                                    .Where(damagedEnemy => damagedEnemy.tag == "Enemy")
                                    .Select(damagedEnemy => damagedEnemy.gameObject.GetComponent<Enemy>());

        foreach (var damagedEnemy in damagedEnemies)
            damagedEnemy.GetDamage(Damage, isPiercingAttack);
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.lossyScale * RadiusAttack);
    }
}
