using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Unit : MonoBehaviour
{
    public string nameUnit;
    public int damage;
    public int purchaseCost;
    public bool isRoadUnit;
    private int levelUnit = 1;
    private int maxLevelUnit = 3;

    [Range(0.1f, 100)]
    [SerializeField] protected float attackSpeed;
    [Range(0, 10)]
    [SerializeField] protected int attackRange;
    [SerializeField] protected int improvementCost;
    [SerializeField] protected bool isPiercingAttack;
    [SerializeField] protected int upDamage;
    [SerializeField] protected int upAttackSpeed;
    protected abstract void Attack();

    [SerializeField] protected Queue<Enemy> targets;
    protected Enemy target;
    protected bool canAttack = true;

    public bool isMaxLevel { get => levelUnit == maxLevelUnit; }

    private void Awake()
    {
        target = null;
        targets = new Queue<Enemy>();
    }

    private void Start()
    {

        GetComponent<BoxCollider>().size = new Vector3(GetComponent<BoxCollider>().size.x * 2 * attackRange + 1,
                                                       GetComponent<BoxCollider>().size.y,
                                                       GetComponent<BoxCollider>().size.z * 2 * attackRange + 1);
    }

    private void Update()
    {
        if (canAttack)
            StartCoroutine(Attacking());
        if (target == null && targets.Count > 0)
            target = targets.Dequeue();
    }

    public void UpLevel()
    {
        damage += upDamage;
        attackSpeed += upAttackSpeed;
    }

    public void BuyUnit()
    {
        EconomicModel.Instance.ReduceÑountCoin(purchaseCost);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Enter " + other.name);
            targets.Enqueue(other.gameObject.GetComponent<Enemy>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            target = null;
        }
    }

    IEnumerator Attacking()
    {
        canAttack = false;
        if (target != null)
        {
            Attack();
            yield return new WaitForSeconds(1 / attackSpeed);
        }
        canAttack = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
    }
}