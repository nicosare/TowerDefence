using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : RoadUnit
{
    private bool isWaitSecond = false;
    private void LateUpdate()
    {
        if(!isWaitSecond)
            StartCoroutine(DamageInSecond());
    }

    IEnumerator DamageInSecond()
    {
        isWaitSecond = true;
        yield return new WaitForSeconds(1);
        GetDamage(1);
        isWaitSecond = false;
    }

    protected override void Attack()
    {
        target.GetDamage(Damage, isPiercingAttack);
        foreach (var target in targets)
            target.GetDamage(Damage, isPiercingAttack);
    }
}
