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
        Beat();
    }
    private void Beat()
    {
        target.GetDamage(damage, isPiercingAttack);
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
    }
}