using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.CanvasScaler;

public abstract class Unit : MonoBehaviour
{
    public string NameUnit;
    public int Damage;
    public int PurchaseCost;
    public int ImprovementCost;
    public int SellCost;
    public bool IsRoadUnit;
    private int levelUnit = 0;
    private int maxLevelUnit = 3;

    [Range(0.1f, 100)]
    [SerializeField] protected float attackSpeed;
    [Range(0, 10)]
    [SerializeField] protected int attackRange;
    [SerializeField] protected bool isPiercingAttack;
    [SerializeField] protected int upDamage;
    [SerializeField] protected int upAttackSpeed;
    protected abstract void Attack();

    [SerializeField] protected Queue<Enemy> targets;
    protected Enemy target;
    [SerializeField] protected bool canAttack = true;

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
        if (EconomicModel.Instance.countCoins >= PurchaseCost)
        {
            var coef = 0.5f;
            levelUnit++;
            Damage += upDamage;
            attackSpeed += upAttackSpeed;
            EconomicModel.Instance.Reduce—ountCoin(ImprovementCost);
            ImprovementCost += (int)Mathf.Ceil(ImprovementCost * levelUnit * coef);
            SellCost += (int)Mathf.Ceil(ImprovementCost * levelUnit * coef);
        }
        else
            Message.Instance.LoadMessage("ÕÂ‰ÓÒÚ‡ÚÓ˜ÌÓ ‰ÂÌÂ„!");
    }

    public void BuyUnit()
    {
        if (EconomicModel.Instance.countCoins >= PurchaseCost)
            EconomicModel.Instance.Reduce—ountCoin(PurchaseCost);
    }

    public void SellUnit()
    {
        EconomicModel.Instance.IncreaseCountCoin(SellCost);
        transform.parent.GetComponent<Place>().isFree = true;
        Destroy(gameObject);
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