using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected int damage;
    protected bool isPiercingAttack;
    protected Transform target;
    protected float shootSpeed;
    protected bool isHitting = true;

    [Range(0.1f, 10)]
    [SerializeField] protected float radiusAttack;
    [Range(1, 10)]
    [SerializeField] protected int hitCount;
    [SerializeField] protected bool isStunning;
    [Range(1, 10)]
    [SerializeField] protected int stunTime;

    [SerializeField] protected ParticleSystem endingParticles;
    [SerializeField] protected ParticleSystem areaParticles;
    public void ApplyUnitParameters(int damage, bool isPiercingAttack, Transform target, float shootSpeed)
    {
        this.damage = damage;
        this.isPiercingAttack = isPiercingAttack;
        this.target = target;
        this.shootSpeed = shootSpeed;
    }

    protected abstract void MoveToTarget();

    private void Update()
    {
        MoveToTarget();
    }

    protected abstract void Hit();
    protected abstract void Hit(Collider other);

    protected IEnumerator HittingAround()
    {
        isHitting = false;
        for (var i = 0; i < hitCount; i++)
        {
            var damagedEnemies = Physics.OverlapBox(transform.position, transform.lossyScale * radiusAttack / 2)
                                    .Where(damagedEnemy => damagedEnemy.tag == "Enemy")
                                    .Select(damagedEnemy => damagedEnemy.gameObject.GetComponent<Enemy>());

            if (areaParticles != null)
            {
                SetAreaParticles();
            }

            foreach (var damagedEnemy in damagedEnemies)
            {
                damagedEnemy.GetDamage(damage, isPiercingAttack);
                if (isStunning)
                    damagedEnemy.StopMove(stunTime);
            }

            if (hitCount == 1)
            {
                if (endingParticles != null)
                    StartCoroutine(DestroyWithParticles());
                else
                    Destroy(gameObject);
            }
            else
            {
                yield return new WaitForSeconds(1);
                if (areaParticles != null)
                    areaParticles.gameObject.SetActive(false);
            }
        }
        if (endingParticles != null)
            StartCoroutine(DestroyWithParticles());
        else
            Destroy(gameObject);
    }

    protected IEnumerator HittingAround(AudioSource audioSource, AudioClip soundDamage)
    {
        isHitting = false;
        for (var i = 0; i < hitCount; i++)
        {
            audioSource.PlayOneShot(soundDamage);
            var damagedEnemies = Physics.OverlapBox(transform.position, transform.lossyScale * radiusAttack / 2)
                                    .Where(damagedEnemy => damagedEnemy.tag == "Enemy")
                                    .Select(damagedEnemy => damagedEnemy.gameObject.GetComponent<Enemy>());

            if (areaParticles != null)
            {
                SetAreaParticles();
            }

            foreach (var damagedEnemy in damagedEnemies)
            {
                damagedEnemy.GetDamage(damage, isPiercingAttack);
                if (isStunning)
                    damagedEnemy.StopMove(stunTime);
            }

            if (hitCount == 1)
            {
                if (endingParticles != null)
                    StartCoroutine(DestroyWithParticles());
                else
                    Destroy(gameObject);
            }
            else
            {
                yield return new WaitForSeconds(1);
                if (areaParticles != null)
                    areaParticles.gameObject.SetActive(false);
            }
        }
        if (endingParticles != null)
            StartCoroutine(DestroyWithParticles());
        else
            Destroy(gameObject);
    }

    private void SetAreaParticles()
    {
        areaParticles.gameObject.SetActive(true);
        var particlesShape = areaParticles.shape;
        var particlesCount = areaParticles.emission.GetBurst(0);
        particlesShape.scale = new Vector3(radiusAttack, radiusAttack);
        particlesCount.count = 100 * radiusAttack * radiusAttack;
    }

    private IEnumerator DestroyWithParticles()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    protected IEnumerator Hitting(Collider other)
    {
        isHitting = false;
        var damagedEnemy = other.gameObject.GetComponent<Enemy>();
        for (var i = 0; i < hitCount; i++)
        {
            if (damagedEnemy != null)
            {
                damagedEnemy.GetDamage(damage, isPiercingAttack);
                if (isStunning)
                    damagedEnemy.StopMove(stunTime);
                if (hitCount == 1)
                {
                    Destroy(gameObject);
                }
                else
                    yield return new WaitForSeconds(1);
            }
        }
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.lossyScale * radiusAttack);
    }
}
