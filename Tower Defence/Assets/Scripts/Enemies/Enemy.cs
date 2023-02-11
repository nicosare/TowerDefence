using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

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

    public Queue<Transform> way;
    private Transform wayPointTarget;
    private IHealth attackTarget;
    private bool isShowHealthBar;

    private Animator animator;
    private ParticleSystem starsAroundHead;

    private void Start()
    {
        var wayPoints = transform.parent.GetChild(0);
        way = new Queue<Transform>();
        foreach (Transform point in wayPoints)
            way.Enqueue(point);
        wayPointTarget = way.Dequeue();
        attackTarget = null;
        healthBar.maxValue = health;
        healthBar.value = health;
        healthBar.gameObject.SetActive(false);

        animator = transform.GetComponentInChildren<Animator>();
        animator.SetFloat("AttackSpeed", attackSpeed);
        animator.SetFloat("Speed", speed);
        starsAroundHead = transform.GetComponentInChildren<ParticleSystem>(true);
    }

    public void GetDamage(int damage, bool isPiercingAttack)
    {
        if (!isArmored || isPiercingAttack)
        {
            health -= damage;
            if (!isShowHealthBar)
                StartCoroutine(ShowHealthBar());
            healthBar.value = health;
        }
        else
            Message.Instance.LoadMessage("Броня не пробита");
        if (health <= 0)
        {
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
        var dir = wayPointTarget.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);
        transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation,
                                                          Quaternion.LookRotation(wayPointTarget.position - transform.position),
                                                          5 * Time.deltaTime);

        if (Vector3.Distance(transform.position, wayPointTarget.position) <= 0.1f)
        {
            if (way.Count > 0)
                wayPointTarget = way.Dequeue();
            else
                isEndWay = true;
        }
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
        starsAroundHead.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeStoppingInSeconds);
        animator.SetBool("IsStun", false);
        isStun = false;
        starsAroundHead.gameObject.SetActive(false);
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
        Debug.DrawRay(transform.position, transform.GetChild(0).forward * rangeAttack, Color.red);

        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, rangeAttack, layerMask))
        {
            canAttack = true;
            canMove = false;
            animator.SetBool("CanMove", false);
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

