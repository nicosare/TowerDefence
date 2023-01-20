using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCastle : MonoBehaviour, IHealth
{
    [SerializeField] private int health;
    public int Health { get => health; set => health = value; }

    public void Die()
    {
        Debug.Log("Вы прогирали");
    }

    public void GetDamage(int damage)
    {
        Health -= damage;
        if (health <= 0)
            Die();
    }
}
