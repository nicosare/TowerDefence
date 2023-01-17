using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private int health;
    private int speed;
    private bool isArmored;
    [SerializeField] private GameObject way;

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

    }
    private void Update()
    {
        MoveToPoints();
    }
}
