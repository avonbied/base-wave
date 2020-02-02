using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : EntityRanged
{
    ContactFilter2D TargetFilter;
    ContactFilter2D TurretFilter;
    ContactFilter2D WallFilter;

    private void Awake()
    {
        TargetFilter = new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.GetMask("Friendly") };
        TurretFilter = new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.GetMask("Turret") };
        WallFilter = new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.GetMask("Wall") };
        Friendly = false;
    }

    public void FixedUpdate()
    {
        if (this.IsDead || Global.GameOver)
        {
            Die();
            return;
        }
        if (AttackingBase)
        {
            AttackBase();
            return;
        }
        if (transform.GetComponent<CircleCollider2D>().OverlapCollider(TurretFilter, Colliders) > 0 ||
    transform.GetComponent<CircleCollider2D>().OverlapCollider(WallFilter, Colliders) > 0)
        {
            AttackBase();
            return;
        }

        if (EnemyTarget == null || Vector3.Distance(transform.position, EnemyTarget.position) > WeaponRange)
            FindNewTarget("Friendly");

        if (EnemyTarget != null)
        {
            FireOnTarget(TargetFilter, false);
            LookAtPosition(EnemyTarget.position);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPos, Speed * Time.fixedDeltaTime);

            LookAtPosition(TargetPos);
        }
    }
}
