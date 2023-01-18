using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage;
    public bool isPiercingAttack;
    public GameObject target;
    public float ShootSpeed;

    private void MoveToTarget()
    {
        var dir = target.transform.position - transform.position;
        transform.Translate(dir.normalized * ShootSpeed * Time.deltaTime);

    }
    private void Update()
    {
        MoveToTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            other.GetComponent<Enemy>().GetDamage(Damage, isPiercingAttack);
        Destroy(gameObject);
    }
}
