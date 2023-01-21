using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadUnit : Unit, IHealth
{
    [SerializeField] protected int health;

    public int Health { get => health; set => health = value; }

    protected override void Attack()
    {
        target.GetDamage(Damage, isPiercingAttack);
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
        transform.parent.GetComponent<Place>().isFree = true;
    }
}