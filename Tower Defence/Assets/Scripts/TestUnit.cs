using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TestUnit : Unit
{
    [Range(2, 10)]
    [SerializeField] private int attackRange;
    [Range(0.1f, 10)]
    [SerializeField] private int shootSpeed;
    [SerializeField] private Bullet bullet;
    private GameObject target;
    private bool canShoot = true;
    private Queue<GameObject> targets;

    private void Awake()
    {
        GetComponent<BoxCollider>().size *= attackRange;
        target = null;
        targets = new Queue<GameObject>();
    }

    private void Update()
    {
        if (canShoot)
            StartCoroutine(Shooting());
    }

    private void OnTriggerEnter(Collider other)
    {
        targets.Enqueue(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        target = null;
    }

    IEnumerator Shooting()
    {
        canShoot = false;
        if (target == null && targets.Count > 0)
            target = targets.Dequeue();
        else
        {
            Shoot();
            yield return new WaitForSeconds(10 / attackSpeed);
        }
        canShoot = true;
    }

    private void Shoot()
    {
        var newBullet = Instantiate(bullet.gameObject, transform).GetComponent<Bullet>();
        newBullet.transform.localPosition = Vector3.zero;
        newBullet.Damage = damage;
        newBullet.isPiercingAttack = isPiercingAttack;
        newBullet.target = target;
        newBullet.ShootSpeed = shootSpeed;

    }
}
