using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Entity
{
    ContactFilter2D Filter;
    private void Awake()
    {
        Filter = new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.GetMask("Enemy") };
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

        if (EnemyTarget == null || Vector3.Distance(transform.position, EnemyTarget.position) > WeaponRange)
            FindNewTarget("Friendly");

        if (EnemyTarget != null)
        {
            FireOnTarget();
            LookAtPosition(EnemyTarget.position);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPos, Speed * Time.fixedDeltaTime);
            LookAtPosition(TargetPos);
            if (transform.GetComponent<CircleCollider2D>().OverlapCollider(new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.GetMask("Turret") }, new List<Collider2D>()) > 0 || transform.GetComponent<CircleCollider2D>().OverlapCollider(new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.GetMask("Wall") }, new List<Collider2D>()) > 0)
            {
                AttackBase();
            }
        }
    }


    public override void FireOnTarget()
    {
        if (TimeLastFired + (1.0f / FireRate) <= Time.realtimeSinceStartup)
        {
            TimeLastFired = Time.realtimeSinceStartup;
            var obj = Global.ProjectilePool.Rent();
            obj.SetActive(true);
            var proj = obj.GetComponent<Projectile>();
            proj.Reset(transform.position, transform.rotation, transform.right * ProjectileSpeed, BaseWeaponRange / ProjectileSpeed);
            proj.ContactFilter = Filter;
        }
    }
}
