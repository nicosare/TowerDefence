using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RoadUnit : Unit, IHealth
{
    public int health;
    [SerializeField] private Slider healthBar;
    [SerializeField] private AudioClip soundDeath;
    [SerializeField] private AudioClip soundGetDamage;

    private bool canSlash = true;
    public int Health { get => health; set => health = value; }

    protected Animator animator;
    private void Awake()
    {
        if (TryGetComponent(out Animator anim))
        {
            animator = anim;
            animator.SetFloat("Speed", AttackSpeed);
        }
    }

    protected override void Attack()
    {
        if (target != null)
            animator.SetBool("Attack", canSlash);
    }

    public void StartAttacking()
    {
        if (target != null)
            StartCoroutine(Slashing());
    }

    private IEnumerator Slashing()
    {
        canSlash = false;
        target.GetDamage(Damage, isPiercingAttack);
        yield return new WaitForSeconds(reloadTime);
        canSlash = true;
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
        audioSource.volume = 0.2f;
        audioSource.PlayOneShot(soundGetDamage);
        if (health <= 0)
        {
            audioSource.volume = 1f;
            audioSource.PlayOneShot(soundDeath);
            if (animator == null)
                Die();
            else
            {
                Destroy(transform.GetComponent<Collider>());
                animator.SetTrigger("Die");
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        transform.parent.GetComponent<Place>().isFree = true;
    }
}