using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUnit : EntityMelee
{
    ContactFilter2D TargetFilter;

    protected override void Start()
    {
        base.Start();
        TargetFilter = new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.GetMask("Enemy") };
        Friendly = true;
    }

    private void FixedUpdate()
    {
        if (this.IsDead || Global.GameOver)
        {
            Die();
            return;
        }

        if ((Vector3.Distance(transform.position, TargetPos) >= 0.1) && (TargetPosDesignated))
        {
            LookAtPosition(TargetPos);
            transform.position = Vector3.MoveTowards(transform.position, TargetPos, Speed * Time.fixedDeltaTime);
        }
        else
        {
            if (EnemyTarget == null || Vector3.Distance(transform.position, EnemyTarget.position) > WeaponRange)
                FindNewTarget("Friendly");

            if (EnemyTarget != null)
            {
                if (Vector3.Distance(transform.position, EnemyTarget.position) < WeaponRange * .4f)
                {
                    FireOnTarget(TargetFilter, true);
                    LookAtPosition(EnemyTarget.position);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, EnemyTarget.position, Speed * Time.fixedDeltaTime);
                    LookAtPosition(EnemyTarget.position);
                }
            }
        }
    }
}