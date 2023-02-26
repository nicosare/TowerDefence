using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : RoadUnit
{
    private bool isWaitSecond = false;
    [SerializeField] protected bool isStunning;
    [Range(1, 3)]
    [SerializeField] protected int stunTime;

    private void LateUpdate()
    {
        if (!isWaitSecond)
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
        animator.SetTrigger("Attack");
        foreach (var target in targets)
        {
            target.GetDamage(Damage, isPiercingAttack);
            if (isStunning)
            {
                StartCoroutine(StunAnim(stunTime));
                target.StopMove(stunTime);
            }
        }
    }

    private IEnumerator StunAnim(int stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        animator.SetTrigger("EndAttack");
    }
}

