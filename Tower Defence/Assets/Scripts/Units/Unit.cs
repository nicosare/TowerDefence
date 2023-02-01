using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.CanvasScaler;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] private string nameUnit;
    [SerializeField] private int damage;
    [SerializeField] private int buyPrice;
    [SerializeField] private bool isRoadUnit;
    [Range(0.1f, 100)]
    public float attackSpeed;
    [Range(0, 10)]
    [SerializeField] protected int attackRange;
    [SerializeField] protected bool isPiercingAttack;
    private int levelUnit = 0;
    private int maxLevelUnit = 3;

    protected abstract void Attack();

    protected List<Enemy> targets;
    protected Enemy target;
    [SerializeField] protected bool canAttack = true;

    public bool IsMaxLevel { get => levelUnit == maxLevelUnit; }
    public int BuyPrice { get => buyPrice; }
    public int UpgradePrice { get => upgradePrice; private set => upgradePrice = value; }
    public int SellPrice { get => sellPrice; private set => sellPrice = value; }
    public int Damage { get => damage; private set => damage = value; }
    public string NameUnit { get => nameUnit; }
    public bool IsRoadUnit { get => isRoadUnit; }

    private int upgradePrice;
    private int sellPrice;
    private float sellPriceCoef = 0.5f;
    private float upgradePriceCoef = 0.6f;
    private float upgradeCoef = 1.5f;


    private void Awake()
    {
        target = null;
        targets = new List<Enemy>();
        SetPrices();
    }

    private void Start()
    {
        GetComponent<BoxCollider>().size = new Vector3(GetComponent<BoxCollider>().size.x * 2 * attackRange + 1,
                                                       GetComponent<BoxCollider>().size.y,
                                                       GetComponent<BoxCollider>().size.z * 2 * attackRange + 1);
    }

    public void UpLevel()
    {
        if (EconomicModel.Instance.countCoins >= UpgradePrice)
        {
            EconomicModel.Instance.Reduce�ountCoin(UpgradePrice);
            levelUnit++;
            Damage = Mathf.CeilToInt(Damage * upgradeCoef);
            attackSpeed = attackSpeed * upgradeCoef;
            SetPrices();
        }
        else
            Message.Instance.LoadMessage("������������ �����!");
    }

    private void SetPrices()
    {
        UpgradePrice += Mathf.CeilToInt(BuyPrice * upgradePriceCoef);
        SellPrice = Mathf.CeilToInt((BuyPrice + UpgradePrice) * sellPriceCoef);
    }

    public void BuyUnit()
    {
        if (EconomicModel.Instance.countCoins >= BuyPrice)
            EconomicModel.Instance.Reduce�ountCoin(BuyPrice);
    }

    public void SellUnit()
    {
        EconomicModel.Instance.IncreaseCountCoin(SellPrice);
        transform.parent.GetComponent<Place>().isFree = true;
        Destroy(gameObject);
    }

    private void Update()
    {
        if (canAttack && target != null)
            StartCoroutine(Attacking());

        targets = FindTargets();
        target = targets.FirstOrDefault();
    }

    private List<Enemy> FindTargets()
    {
        return Physics.OverlapBox(transform.position, GetComponent<BoxCollider>().size / 2)
                                    .Where(target => target.tag == "Enemy")
                                    .Select(target => target.gameObject.GetComponent<Enemy>())
                                    .OrderBy(target => target.way.Count)
                                    .ToList();
    }

    IEnumerator Attacking()
    {
        canAttack = false;
        Attack();
        yield return new WaitForSeconds(1 / attackSpeed);
        canAttack = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);

        if (target != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }
}