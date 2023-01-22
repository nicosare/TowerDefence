using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected int damage;
    protected bool isPiercingAttack;
    protected Transform target;
    protected float shootSpeed;

    [Range(1, 10)]
    [SerializeField] protected float RadiusAttack;

    public void ApplyUnitParameters(int damage, bool isPiercingAttack, Transform target, float shootSpeed)
    {
        this.damage = damage;
        this.isPiercingAttack = isPiercingAttack;
        this.target = target;
        this.shootSpeed = shootSpeed;
    }

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
            damagedEnemy.GetDamage(damage, isPiercingAttack);
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.lossyScale * RadiusAttack);
    }
}
