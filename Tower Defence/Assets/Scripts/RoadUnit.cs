using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadUnit : Unit
{
    protected override void Attack()
    {
        Beat();
    }

    private void Beat()
    {
        target.GetComponent<Enemy>().GetDamage(damage, isPiercingAttack);
    }
}
