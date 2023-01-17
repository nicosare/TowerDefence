using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int speed;
    [SerializeField] protected bool isArmored;

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

    public void GetDamsge(int damage, bool isPiercingAttack)
    {
        if (!isArmored || isPiercingAttack)
            health -= damage;
        else
            Debug.Log("броня не добита"); //Сделать вывод на экран сообщения, что броня не пробита

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(this);
    }

    private void MoveToPoints()
    {
        var dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) <= 0.01f)
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
