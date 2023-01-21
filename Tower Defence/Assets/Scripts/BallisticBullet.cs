using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallisticBullet : Bullet
{

    [Range(1, 10)]
    [SerializeField] protected float RadiusAttack;

    private Vector3 firstMiddlePoint;
    private Vector3 secondMiddlePoint;
    private Vector3 point1;
    private Vector3 point2;
    private Vector3 point3;
    private Vector3 point4;
    private Vector3 point5;
    private Vector3 followPoint;
    private bool canMove;
    private float step;
    private Vector3 targetPoint;

    private void Start()
    {
        targetPoint = target.position;
        firstMiddlePoint = Vector3.Lerp(transform.parent.position, targetPoint, .25f) + 2 * Vector3.up.normalized;
        secondMiddlePoint = Vector3.Lerp(transform.parent.position, targetPoint, .75f) + 2 * Vector3.up.normalized;
        step = ShootSpeed * Time.deltaTime;
        canMove = true;
        point1 = transform.parent.position;
        point2 = firstMiddlePoint;
        point3 = secondMiddlePoint;
        point4 = point1;
        point5 = point2;
        followPoint = point4;
    }
    protected override void MoveToTarget()
    {
        if (canMove)
        {
            if (point4.y > transform.position.y)
                step *= 1.01f;
            else if (point4.y < transform.position.y)
                step *= 0.09f;
            canMove = false;
            point1 = Vector3.MoveTowards(point1, firstMiddlePoint, step);
            point2 = Vector3.MoveTowards(point2, secondMiddlePoint, step);
            point3 = Vector3.MoveTowards(point3, targetPoint, step);
            point4 = Vector3.MoveTowards(point4, point2, step);
            point5 = Vector3.MoveTowards(point5, point3, step);
            followPoint = Vector3.MoveTowards(followPoint, point5, step);
            transform.position = Vector3.MoveTowards(transform.position, followPoint, step);
            canMove = true;
        }
    }

    protected override void Hit()
    {
        var damagedEnemies = Physics.OverlapBox(transform.position, transform.lossyScale * RadiusAttack / 2);

        foreach (var damagedEnemy in damagedEnemies)
            if (damagedEnemy.tag == "Enemy")
            {
                damagedEnemy.gameObject.GetComponent<Enemy>().GetDamage(Damage, isPiercingAttack);
            }
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.lossyScale * RadiusAttack);
        Gizmos.DrawCube(firstMiddlePoint, new Vector3(.15f, .15f, .15f));
        Gizmos.DrawCube(secondMiddlePoint, new Vector3(.15f, .15f, .15f));

        Gizmos.color = Color.blue;
        Gizmos.DrawCube(point1, new Vector3(.15f, .15f, .15f));
        Gizmos.DrawCube(point2, new Vector3(.15f, .15f, .15f));
        Gizmos.DrawCube(point3, new Vector3(.15f, .15f, .15f));
        Gizmos.DrawCube(point4, new Vector3(.15f, .15f, .15f));
        Gizmos.DrawCube(point5, new Vector3(.15f, .15f, .15f));

        Gizmos.color = Color.green;
        Gizmos.DrawCube(followPoint, new Vector3(.15f, .15f, .15f));

    }
}
