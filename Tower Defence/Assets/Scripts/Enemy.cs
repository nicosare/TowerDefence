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
    protected bool isEndWay = false;
    [Range(0.1f, 100)]
    [SerializeField] protected float attackSpeed;
    public int damage;
    public LayerMask layerMask;
    [SerializeField] private Slider healthBar;

    private Queue<Transform> way;
    private Transform wayPointTarget;
    private IHealth attackTarget;
    private bool isShowHealthBar;

    private void Start()
    {
        var Waypoints = transform.parent.GetComponent<WaveSpawner>().WayPoints;
        way = new Queue<Transform>();
        foreach (var point in Waypoints)
            way.Enqueue(point);
        wayPointTarget = way.Dequeue();
        attackTarget = null;
        healthBar.maxValue = health;
        healthBar.value = health;
        healthBar.gameObject.SetActive(false);
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
            Die();
    }

    IEnumerator ShowHealthBar()
    {
        isShowHealthBar = true;
        healthBar.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        healthBar.gameObject.SetActive(false);
        isShowHealthBar = false;
    }
    private void Die()
    {
        Destroy(gameObject);
        EconomicModel.Instance.IncreaseCountCoin(coinKill);
    }

    private void MoveToPoints()
    {
        var dir = wayPointTarget.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);
        transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, Quaternion.LookRotation(wayPointTarget.position - transform.position), 3 * Time.deltaTime);

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
        FindAttackTarget();

        if (canAttack && attackTarget != null)
            StartCoroutine(Attacking());

        if (canMove && !isEndWay)
            MoveToPoints();
    }

    IEnumerator Attacking()
    {
        canAttack = false;
        if (attackTarget != null)
        {
            Attack();
            yield return new WaitForSeconds(1 / attackSpeed);
        }
        canAttack = true;
    }

    public void StopMove(int timeStoppingInSeconds)
    {
        canMove = false;
        StartCoroutine(Stopping(timeStoppingInSeconds));
    }

    IEnumerator Stopping(int timeStoppingInSeconds)
    {
        yield return new WaitForSeconds(timeStoppingInSeconds);
        canMove = true;
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