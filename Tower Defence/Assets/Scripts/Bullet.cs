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

    protected abstract void MoveToTarget();
    protected abstract void Hit();


    private void Update()
    {
        if (target != null)
        {
            MoveToTarget();
        }
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            Hit();
        else
            Destroy(gameObject);
    }
}
