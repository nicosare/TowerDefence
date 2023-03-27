using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallisticBullet : Bullet
{

    private Vector3 middlePoint;
    private Vector3 point1;
    private Vector3 point2;
    private Vector3 targetPosition;
    private AudioSource audioSource;
    [SerializeField] private AudioClip soundLandingBullet;
    [SerializeField] private AudioClip soundDamage;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        targetPosition = target.position;
        middlePoint = Vector3.Lerp(transform.position, targetPosition, .5f) + 3 * Vector3.up.normalized;
        point1 = transform.position;
        point2 = middlePoint;
    }

    private void RotateModel()
    {
        transform.GetChild(0).Rotate(new Vector3(45, 0, 0) * 5 * Time.deltaTime);
    }

    protected override void MoveToTarget()
    {
        RotateModel();
        point1 = Vector3.MoveTowards(point1, middlePoint, shootSpeed * Time.deltaTime);
        point2 = Vector3.MoveTowards(point2, targetPosition, shootSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, point2, shootSpeed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            if (endingParticles != null)
            {
                endingParticles.gameObject.SetActive(true);
            }
            transform.GetChild(0).gameObject.SetActive(false);
            Hit();
        }
    }

    protected override void Hit()
    {
        if (isHitting)
        {
            audioSource.PlayOneShot(soundLandingBullet);
            if (gameObject.name.Contains("Poison"))
                StartCoroutine(HittingAround(audioSource, soundDamage));
            else
                StartCoroutine(HittingAround());
        }
    }

    protected override void Hit(Collider other)
    {
        throw new System.NotImplementedException();
    }
}
