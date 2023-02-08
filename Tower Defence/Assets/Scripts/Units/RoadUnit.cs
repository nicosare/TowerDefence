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

    protected Animator animator;
    private void Awake()
    {
        if (TryGetComponent(out Animator anim))
            animator = anim;
    }

    protected override void Attack()
    {
        animator.SetTrigger("Attack");
        target.GetDamage(Damage, isPiercingAttack);
    }

    private void Start()
    {
        healthBar.maxValue = Health;
        healthBar.value = Health;
    }
    public void GetDamage(int damage)
    {
        health -= damage;
        healthBar.value = health;
        if (health <= 0)
        {
            if (animator == null)
                Die();
            else
                animator.SetTrigger("Die");
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        transform.parent.GetComponent<Place>().isFree = true;
    }
}