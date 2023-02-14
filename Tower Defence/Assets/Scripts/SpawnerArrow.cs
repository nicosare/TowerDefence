using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerArrow : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] int attackSpeed;
    private bool isAttack = false;

    void Update()
    {
        if (!isAttack)
            StartCoroutine(SpawnArrow());
    }

    IEnumerator SpawnArrow()
    {
        isAttack = true;
        Instantiate(arrow, transform.position, transform.rotation);
        yield return new WaitForSeconds(1 / attackSpeed);
        isAttack = false;
    }
}
