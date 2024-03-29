﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [Range(0f, 4f)]
    [SerializeField] protected float speed;
    [SerializeField] protected bool isArmored;
    [SerializeField] protected int coinKill;
    [SerializeField] protected float rangeAttack;
    protected bool canAttack = true;
    protected bool canMove = true;
    protected bool isStun = false;
    protected bool isEndWay = false;
    [Range(0.1f, 100)]
    [SerializeField] protected float attackSpeed;
    public int damage;
    public LayerMask layerMask;
    [SerializeField] private Slider healthBar;
    private AudioSource audioSource;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;

    public Queue<Vector3> way;
    private Vector3 wayPointTarget;
    private IHealth attackTarget;
    private bool isShowHealthBar;

    private Animator animator;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.2f;
        var wayPoints = transform.parent.GetChild(0);
        way = new Queue<Vector3>();
        foreach (Transform point in wayPoints)
            way.Enqueue(point.position);
        wayPointTarget = way.Dequeue();
        attackTarget = null;
        healthBar.maxValue = health;
        healthBar.value = health;
        healthBar.gameObject.SetActive(false);

        animator = transform.GetComponentInChildren<Animator>();
        animator.SetFloat("AttackSpeed", attackSpeed);
        animator.SetFloat("Speed", speed);
    }

    public void GetDamage(int damage, bool isPiercingAttack)
    {
        if (!isArmored || isPiercingAttack)
        {
            health -= damage;
            audioSource.PlayOneShot(hurtSound);
            if (!isShowHealthBar)
                StartCoroutine(ShowHealthBar());
            healthBar.value = health;
        }
        else
        {
            if (LocalizationSettings.Instance.GetSelectedLocale() == LocalizationSettings.AvailableLocales.Locales[0])
                Message.Instance.LoadMessage("Armor is not pierced");
            else
                Message.Instance.LoadMessage("Броня не пробита");
        }
        if (health <= 0)
        {
            audioSource.volume = 0.5f;
            audioSource.PlayOneShot(deathSound);
            Destroy(transform.GetComponent<Collider>());
            animator.SetTrigger("Die");
            canMove = false;
            canAttack = false;
        }
    }

    IEnumerator ShowHealthBar()
    {
        isShowHealthBar = true;
        healthBar.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        healthBar.gameObject.SetActive(false);
        isShowHealthBar = false;
    }
    public void Die()
    {
        EconomicModel.Instance.IncreaseCountCoin(coinKill);
        Destroy(gameObject);
    }

    private void MoveToPoints()
    {
        var dir = wayPointTarget - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);
        transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation,
                                                          Quaternion.LookRotation(wayPointTarget - transform.position),
                                                          5 * Time.deltaTime);

        if (Vector3.Distance(transform.position, wayPointTarget) <= 0.1f)
        {
            if (way.Count > 0)
                wayPointTarget = MoveRandomPoint();
            else
                isEndWay = true;
        }
    }

    private Vector3 MoveRandomPoint()
    {
        var center = way.Dequeue();
        var newTarget = center + new Vector3(Random.Range(-0.25f, 0.25f), 0, Random.Range(-0.25f, 0.25f));
        return newTarget;
    }
    private void Update()
    {
        if (!isStun)
        {
            FindAttackTarget();
            if (canAttack && attackTarget != null)
                animator.SetTrigger("Attack");
            if (canMove && !isEndWay)
                MoveToPoints();
        }
    }
    public void StopMove(int timeStoppingInSeconds = 1)
    {
        isStun = true;
        animator.SetBool("IsStun", true);
        StartCoroutine(Stopping(timeStoppingInSeconds));
    }
    IEnumerator Stopping(int timeStoppingInSeconds)
    {
        yield return new WaitForSeconds(timeStoppingInSeconds);
        animator.SetBool("IsStun", false);
        isStun = false;
    }

    public void Attack()
    {
        if (attackTarget != null)
            attackTarget.GetDamage(damage);
        canAttack = false;
    }

    private void FindAttackTarget()
    {
        var ray = new Ray(transform.position, transform.GetChild(0).forward * rangeAttack);
        Debug.DrawRay(transform.position, transform.GetChild(0).forward * rangeAttack, UnityEngine.Color.red);

        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, rangeAttack, layerMask))
        {
            canAttack = true;
            if (name.Contains("Ghost"))
            {
                if (raycastHit.collider.name == "End")
                {
                    canMove = false; 
                    animator.SetBool("CanMove", false);
                }
                else
                    canMove = true;
            }
            else
            {
                canMove = false;
                animator.SetBool("CanMove", false);
            }
            attackTarget = raycastHit.collider.gameObject.GetComponent<IHealth>();
        }
        else
        {
            canAttack = false;
            canMove = true;
            animator.SetBool("CanMove", true);
            attackTarget = null;
        }
    }
}

