using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    private string nameUnit;
    private int damage;
    private int attackSpeed;
    private int levelUnit = 1;
    private int maxLevelUnit = 3;
    private int purchaseCost;
    private int improvementCost;
    private bool isPiercingAttack;
    private int upDamage;
    private int upAttackSpeed;
    public bool isMaxLevel { get => levelUnit == maxLevelUnit; }

    public void UpLevel()
    {
        damage += upDamage;
        attackSpeed += upAttackSpeed;
    }
}
