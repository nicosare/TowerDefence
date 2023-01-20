using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [Range(0.5f, 4f)]
    [SerializeField] protected float speed;
    [SerializeField] protected bool isArmored;
    [SerializeField] protected int coinKill;
    protected bool canAttack = true;
    protected bool isEndWay = false;
    [Range(0.1f, 100)]
    [SerializeField] protected float attackSpeed;
    public int damage;

    private Queue<Transform> way;
    private Transform wayPointTarget;
    private IHealth attackTarget;

    private void Awake()
    {
        var Waypoints = GameObject.FindGameObjectWithTag("Way").transform;
        way = new Queue<Transform>();
        foreach (Transform point in Waypoints)
            way.Enqueue(point);
    }

    private void Start()
    {
        wayPointTarget = way.Dequeue();
    }

    public void GetDamage(int damage, bool isPiercingAttack)
    {
        if (!isArmored || isPiercingAttack)
            health -= damage;
        else
            Message.Instance.LoadMessage("����� �� �������"); //������� ����� �� ����� ���������, ��� ����� �� �������

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
        EconomicModel.Instance.Increase�ountCoin(coinKill);
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
        if (canAttack)
        {
            if (attackTarget != null)
                StartCoroutine(Attacking());
            if (!isEndWay)
                MoveToPoints();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RoadUnit")
            attackTarget = other.gameObject.GetComponent<IHealth>();
        Debug.Log("collision");
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

    private void Attack()
    {
        attackTarget.GetDamage(damage);
    }
}
