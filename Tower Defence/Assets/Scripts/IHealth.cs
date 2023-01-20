using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    public int Health { get; set; }
    public void Die();
    public void GetDamage(int damage);
}
