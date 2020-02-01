using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Entity
{

    // Use this for initialization

    void Start()
    {

    }

    public void FixedUpdate()
    {
        if (HitPoints <= 0)
        {
            Die();
            return;
        }

        if (EnemyTarget == null || Vector3.Distance(transform.position, EnemyTarget.position) > WeaponRange)
            FindNewTarget("Friendly");

        if (EnemyTarget != null)
        {
            FireOnTargetRanged();
            LookAtPosition(EnemyTarget.position);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPos, Speed * Time.fixedDeltaTime);
            LookAtPosition(TargetPos);
        }
    }
}
