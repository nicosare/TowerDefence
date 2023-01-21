using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadUnit : Unit, IHealth
{
    [SerializeField] protected int health;
    [SerializeField] private Slider healthBar;

    public int Health { get => health; set => health = value; }

    protected override void Attack()
    {
        target.GetDamage(damage, isPiercingAttack);
    }

    private void Start()
    {
        healthBar.maxValue = health;
        healthBar.value = health;
    }
    public void GetDamage(int damage)
    {
        health -= damage;
        healthBar.value = health;
        Debug.Log(health);
        if (health <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}