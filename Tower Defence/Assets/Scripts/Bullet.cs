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
    private bool isHitting = true;

    [Range(1, 10)]
    [SerializeField] protected float RadiusAttack;
    [Range(1, 3)]
    [SerializeField] protected int hitCount;
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
        if (isHitting)
            StartCoroutine(Hitting());
    }

    IEnumerator Hitting()
    {
        isHitting = false;
        for (var i = 0; i < hitCount; i++)
        {
            var damagedEnemies = Physics.OverlapBox(transform.position, transform.lossyScale * RadiusAttack / 2)
                                    .Where(damagedEnemy => damagedEnemy.tag == "Enemy")
                                    .Select(damagedEnemy => damagedEnemy.gameObject.GetComponent<Enemy>());

            foreach (var damagedEnemy in damagedEnemies)
            {
                damagedEnemy.GetDamage(damage, isPiercingAttack);
            }
            if (hitCount == 1)
                Destroy(gameObject);
            else
                yield return new WaitForSeconds(1);
        }
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.lossyScale * RadiusAttack);
    }
}
