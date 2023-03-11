using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class UltimateBullet : MonoBehaviour
{
    private Stack<Transform> way;
    private Transform wayPointTarget;
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    [SerializeField] protected bool isPiercingAttack;
    [SerializeField] protected int waitSeconds;

    [Range(1, 10)]
    [SerializeField] protected float RadiusAttack;

    public List<Transform> WayPoints;

    private void Start()
    {
        way = new Stack<Transform>();
        foreach (var point in WayPoints)
            way.Push(point);
        wayPointTarget = way.Pop();
    }
    public void Attack()
    {
        Instantiate(gameObject);
    }

    private void Update()
    {
        MoveToPoints();
    }
    private void MoveToPoints()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPointTarget.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation,
                                                          Quaternion.LookRotation(wayPointTarget.position - transform.position),
                                                          5 * Time.deltaTime);

        if (Vector3.Distance(transform.position, wayPointTarget.position) <= 0.1f)
        {
            if (way.Count > 0)
                wayPointTarget = way.Pop();
            else
                StartCoroutine(WaitAndDistroy(waitSeconds));
        }
    }

    IEnumerator WaitAndDistroy(int seconds)
    {
        GetComponent<Collider>().enabled = false;
        if (transform.GetChild(0).name == "Particle System")
            transform.GetChild(0).GetComponent<ParticleSystem>().startSize = 0;
        yield return new WaitForSeconds(seconds);
        if (SceneManager.GetActiveScene().name == "HowToPlayLevel")
            FindObjectOfType<HowToPlayLevelManager>().NextSlideWithEndUltimate();
        Destroy(gameObject);
    }

    protected void Hit(Collider other)
    {
        other.gameObject.GetComponent<Enemy>().GetDamage(damage, isPiercingAttack);
    }

    private void OnTriggerEnter(Collider other)
    {
        Hit(other);
    }
}
