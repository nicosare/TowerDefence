using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.CanvasScaler;

public abstract class Unit : MonoBehaviour
{
    public Sprite Icon;
    [SerializeField] private string nameUnit;
    [SerializeField] private int damage;
    [SerializeField] private int buyPrice;
    [SerializeField] private string description;
    public enum TypeUnit
    {
        Melee,
        Ranged,
        Wall,
        Trap,
        Magician
    };
    public TypeUnit Type;
    protected abstract void Attack();
    [SerializeField] private bool isRoadUnit;
    [Range(0.1f, 100)]
    public float AttackSpeed;
    [Range(0.01f, 10)]
    [SerializeField] protected float reloadTime;
    [Range(0, 10)]
    public int AttackRange;
    [SerializeField] protected bool isPiercingAttack;
    private int levelUnit = 0;
    private int maxLevelUnit = 3;
    protected AudioSource audioSource;
    [SerializeField] protected AudioClip soundInstallation;
    [SerializeField] protected AudioClip soundUpLevel;
    [SerializeField] protected AudioClip soundSellUnit;

    protected List<Enemy> targets;
    protected Enemy target;
    [SerializeField] protected bool canAttack = true;
    [SerializeField] private GameObject upgradeStar;
    private Transform starsField;
    [SerializeField] protected bool noRotating;

    public bool IsMaxLevel { get => levelUnit == maxLevelUnit; }
    public int BuyPrice { get => buyPrice; }
    public int UpgradePrice { get => upgradePrice; private set => upgradePrice = value; }
    public int SellPrice { get => sellPrice; private set => sellPrice = value; }
    public int Damage { get => damage; private set => damage = value; }
    public string NameUnit { get => nameUnit; }
    public bool IsRoadUnit { get => isRoadUnit; }
    public string Description { get => description; }

    private int upgradePrice;
    private int sellPrice;
    private float sellPriceCoef = 0.5f;
    private float upgradePriceCoef = 0.6f;
    private float upgradeCoef = 1.5f;

    private void Awake()
    {
        target = null;
        targets = new List<Enemy>();
    }

    private void Start()
    {
        SetPrices();
        starsField = transform.GetChild(1);
        GetComponent<BoxCollider>().size = new Vector3(GetComponent<BoxCollider>().size.x * 2 * AttackRange + 1,
                                                       GetComponent<BoxCollider>().size.y,
                                                       GetComponent<BoxCollider>().size.z * 2 * AttackRange + 1);
    }

    public void UpLevel()
    {
        if (EconomicModel.Instance.countCoins >= UpgradePrice)
        {
            audioSource.PlayOneShot(soundUpLevel);
            Instantiate(upgradeStar, starsField);
            EconomicModel.Instance.Reduce�ountCoin(UpgradePrice);
            levelUnit++;
            Damage = Mathf.CeilToInt(Damage * upgradeCoef);
            AttackSpeed = AttackSpeed * upgradeCoef;
            reloadTime = reloadTime / upgradeCoef;
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

    public void PlaySoundInstallation()
    {
        audioSource = GetComponent<AudioSource>();
        if (gameObject.name.Contains("Stone Wall"))
            StartCoroutine(PlaySoundRepeatInstallation(3, 0.3f));
        else if (gameObject.name.Contains("Trap"))
            StartCoroutine(PlaySoundRepeatInstallation(9, 0.1f));
        else
            audioSource.PlayOneShot(soundInstallation);
    }

    IEnumerator PlaySoundRepeatInstallation(int countRepeatSound, float deltaTime)
    {
        while (countRepeatSound > 0)
        {
            audioSource.PlayOneShot(soundInstallation);
            countRepeatSound--;
            yield return new WaitForSeconds(deltaTime);
        }
    }

    public void SellUnit()
    {
        StartCoroutine(PlaySoundSellUnit());
    }

    IEnumerator PlaySoundSellUnit()
    {
        audioSource.PlayOneShot(soundSellUnit);
        yield return new WaitForSeconds(0.25f);
        EconomicModel.Instance.IncreaseCountCoin(SellPrice);
        transform.parent.GetComponent<Place>().isFree = true;
        Destroy(gameObject);
    }

    private void Update()
    {
        if (canAttack && target != null)
            StartCoroutine(Attacking());

    }

    private void OnTriggerStay(Collider other)
    {
        targets = FindTargets();
        target = targets.FirstOrDefault();
    }

    private void OnTriggerExit(Collider other)
    {
        targets = FindTargets();
        target = targets.FirstOrDefault();
    }

    private List<Enemy> FindTargets()
    {
        if (TryGetComponent(out BoxCollider boxCollider))
            return Physics.OverlapBox(transform.position, boxCollider.size / 2)
                                        .Where(target => target.tag == "Enemy")
                                        .Select(target => target.gameObject.GetComponent<Enemy>())
                                        .OrderBy(target => target.way.Count)
                                        .ToList();
        else
        {
            target = null;
            return null;
        }
    }

    IEnumerator Attacking()
    {
        canAttack = false;
        Attack();
        yield return new WaitForSeconds(1 / AttackSpeed);
        canAttack = true;
    }


    private void LateUpdate()
    {
        if (!noRotating)
            Rotating();
    }

    private void Rotating()
    {
        if (target != null)
        {
            var newDir = new Vector3(target.transform.position.x, transform.GetChild(0).position.y, target.transform.position.z);
            transform.GetChild(0).LookAt(newDir);
        }
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