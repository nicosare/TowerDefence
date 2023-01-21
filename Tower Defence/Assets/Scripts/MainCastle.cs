using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCastle : MonoBehaviour, IHealth
{
    [Range(0,500)]
    [SerializeField] private int health;
    [SerializeField] private Text healthUIText;

    private void Start()
    {
        PrintHealthInUI();
    }
    public int Health { get => health; set => health = value; }

    public void Die()
    {
        Debug.Log("Вы прогирали");
    }

    public void GetDamage(int damage)
    {
        Health -= damage;
        PrintHealthInUI();
        if (health <= 0)
            Die();
    }

    private void PrintHealthInUI()
    {
        healthUIText.text = health.ToString();
    }
}
