using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public string nameUnit;
    public int damage;
    public int purchaseCost;

    private int levelUnit = 1;
    private int maxLevelUnit = 3;
    [Range(1, 100)]
    [SerializeField] protected int attackSpeed;
    [SerializeField] protected int improvementCost;
    [SerializeField] protected bool isPiercingAttack;
    [SerializeField] protected int upDamage;
    [SerializeField] protected int upAttackSpeed;
    public bool isMaxLevel { get => levelUnit == maxLevelUnit; }

    public void UpLevel()
    {
        damage += upDamage;
        attackSpeed += upAttackSpeed;
    }

    public void BuyUnit()
    {
        EconomicModel.Instance.Reduce—ountCoin(purchaseCost);
    }
}