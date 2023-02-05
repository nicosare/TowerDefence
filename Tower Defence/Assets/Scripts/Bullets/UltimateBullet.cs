using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UltimateBullet : MonoBehaviour
{
    private Stack<Transform> way;
    private Transform wayPointTarget;
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    [SerializeField] protected bool isPiercingAttack;

    [Range(1, 10)]
    [SerializeField] protected float RadiusAttack;


    private void Start()
    {
        var wayPoints = transform.parent.GetComponent<MainCastle>().WayPoints;
        way = new Stack<Transform>();
        foreach (var point in wayPoints)
            way.Push(point);
        wayPointTarget = way.Pop();
    }
    public void Attack()
    {
        Instantiate(gameObject);
    }

    private void Update()
    {
        MoveToPoints();
    }
    private void MoveToPoints()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPointTarget.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, wayPointTarget.position) <= 0.1f)
        {
            if (way.Count > 0)
                wayPointTarget = way.Pop();
            else
                Destroy(gameObject);
        }
    }

    protected void Hit(Collider other)
    {
        other.gameObject.GetComponent<Enemy>().GetDamage(damage, isPiercingAttack);
    }

    private void OnTriggerEnter(Collider other)
    {
        Hit(other);
    }
}
