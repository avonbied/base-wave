using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedUnit : Entity
{
    ContactFilter2D Filter;
    private void Awake()
    {
        Filter = new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.GetMask("Enemy") };
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
            proj.Damage = Damage;
        }
    }

    public void FixedUpdate()
    {
        if (this.IsDead || Global.GameOver)
        {
            Die();
            return;
        }

        if (Vector3.Distance(transform.position, TargetPos) >= 0.1)
        {
            LookAtPosition(TargetPos);
            transform.position = Vector3.MoveTowards(transform.position, TargetPos, Speed * Time.fixedDeltaTime);
            if (transform.GetComponent<CircleCollider2D>().OverlapCollider(new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.GetMask("Turret") }, new List<Collider2D>()) > 0)
            {
                WeaponRange = BaseWeaponRange * 3;
            }
            else if (transform.GetComponent<CircleCollider2D>().OverlapCollider(new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.GetMask("Wall") }, new List<Collider2D>()) > 0)
            {
                WeaponRange = BaseWeaponRange * 1.5f;
            }
            else
            {
                WeaponRange = BaseWeaponRange;
            }
        }
        else
        {
            if (EnemyTarget == null || Vector3.Distance(transform.position, EnemyTarget.position) > WeaponRange || !EnemyTarget.gameObject.activeSelf)
            {
                FindNewTarget("Enemy");
            }
            if (EnemyTarget != null)
            {
                FireOnTarget();
                LookAtPosition(EnemyTarget.position);
            }
        }

        //if (EnemyTarget != null)
        //{
        //    if (Vector3.Distance(transform.position, EnemyTarget.position) <= WeaponRange)
        //    {
        //        FireOnTargetRanged();
        //        transform.eulerAngles = new Vector3(0, 0, Vector3.Angle(transform.position, EnemyTarget.position));
        //    }
        //    transform.position =  Vector3.MoveTowards(transform.position, TargetPos, Speed * Time.fixedDeltaTime);
        //    transform.eulerAngles = new Vector3(0, 0, Vector3.Angle(transform.position, TargetPos));
        //}
        //else
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, TargetPos, Speed * Time.fixedDeltaTime);
        //    transform.eulerAngles = new Vector3(0, 0, Vector3.Angle(transform.position, TargetPos));
        //}
    }
}
