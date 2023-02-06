using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    }

    public void GetDamage(int damage, bool isPiercingAttack)
    {
        if (!isArmored || isPiercingAttack)
        {
            animator.SetTrigger("GetDamage");
            health -= damage;
            if (!isShowHealthBar)
                StartCoroutine(ShowHealthBar());
            healthBar.value = health;
        }
        else
            Message.Instance.LoadMessage("Броня не пробита");
        if (health <= 0)
        {
            StopMove();
            animator.SetTrigger("Die");
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
        Destroy(gameObject);
        EconomicModel.Instance.IncreaseCountCoin(coinKill);
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
                StartCoroutine(Attacking());

            if (canMove && !isEndWay)
                MoveToPoints();
        }
    }

    IEnumerator Attacking()
    {
        canAttack = false;
        animator.SetBool("Attack", true);
        if (attackTarget != null)
        {
            Attack();
            yield return new WaitForSeconds(1 / attackSpeed);
        }
        canAttack = true;
        animator.SetBool("Attack", false);
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

    private void Attack()
    {
        attackTarget.GetDamage(damage);
    }

    private void FindAttackTarget()
    {
        var ray = new Ray(transform.position, transform.GetChild(0).forward * rangeAttack);
        Debug.DrawRay(transform.position, transform.GetChild(0).forward * rangeAttack, Color.red);

        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, rangeAttack, layerMask))
        {
            attackTarget = raycastHit.collider.gameObject.GetComponent<IHealth>();
            canMove = false;
        }
        else
        {
            canMove = true;
            attackTarget = null;
        }
    }
}
