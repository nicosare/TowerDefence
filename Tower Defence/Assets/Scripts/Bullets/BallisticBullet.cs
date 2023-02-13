using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallisticBullet : Bullet
{
    [SerializeField] private ParticleSystem areaParticles;

    private Vector3 middlePoint;
    private Vector3 point1;
    private Vector3 point2;
    private float step;

    private Vector3 targetPosition;

    private void Start()
    {
        step = shootSpeed * 0.01f;
        targetPosition = target.position;
        middlePoint = Vector3.Lerp(transform.position, targetPosition, .5f) + 2 * Vector3.up.normalized;
        point1 = transform.position;
        point2 = middlePoint;
        transform.position = point1;
    }

    private void RotateModel()
    {
        transform.GetChild(0).Rotate(new Vector3(45, 0, 0) * 5 * Time.deltaTime);
    }

    protected override void MoveToTarget()
    {
        RotateModel();
        point1 = Vector3.MoveTowards(point1, middlePoint, step);
        point2 = Vector3.MoveTowards(point2, targetPosition, step);
        transform.position = Vector3.MoveTowards(transform.position, point2, step);

        if (transform.position == targetPosition)
        {
            if (endingParticles != null)
            {
                endingParticles.gameObject.SetActive(true);
            }
            if (areaParticles != null)
            {
                areaParticles.gameObject.SetActive(true);
                var particlesShape = areaParticles.shape;
                var particlesCount = areaParticles.emission;
                particlesShape.scale = new Vector3(radiusAttack, radiusAttack);
                particlesCount.rateOverTime = 100 * radiusAttack;
            }
            transform.GetChild(0).gameObject.SetActive(false);
            Hit();
        }
    }

    protected override void Hit()
    {
        if (isHitting)
            StartCoroutine(HittingAround());
    }

    protected override void Hit(Collider other)
    {
        throw new System.NotImplementedException();
    }
}
