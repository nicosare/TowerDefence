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

    private Queue<Transform> way;
    private Transform target;

    private void Awake()
    {
        var Waypoints = GameObject.FindGameObjectWithTag("Way").transform;
        way = new Queue<Transform>();
        foreach (Transform point in Waypoints)
            way.Enqueue(point);
    }

    private void Start()
    {
        target = way.Dequeue();
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
        var dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);
        transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, Quaternion.LookRotation(target.position - transform.position), 3 * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            if (way.Count > 0)
                target = way.Dequeue();
            else
                Die();
        }
    }

    private void Update()
    {
        MoveToPoints();
    }
}
