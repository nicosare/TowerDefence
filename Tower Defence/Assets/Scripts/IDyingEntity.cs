using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDyingEntity
{
    [SerializeField] protected int health { get; set; }
    protected void Die();
    public void GetDamage();
}
