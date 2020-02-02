using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedUnit : EntityRanged
{
    ContactFilter2D EnemyFilter;
    ContactFilter2D WallFilter;
    ContactFilter2D TurretFilter;
    
    private void Awake()
    {
        EnemyFilter = new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.GetMask("Enemy") };
        WallFilter = new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.GetMask("Wall") };
        TurretFilter = new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.GetMask("Turret") };
    }
    
    public bool vfxtest = false;
    public void FixedUpdate()
    {
        if (!vfxtest) //Testing purposes
            if (this.IsDead || Global.GameOver)
            {
                Die();
                return;
            }

        Colliders.Clear();

        if ((Vector3.Distance(transform.position, TargetPos) >= 0.1) && (TargetPosDesignated))
        {
            LookAtPosition(TargetPos);
            transform.position = Vector3.MoveTowards(transform.position, TargetPos, Speed * Time.fixedDeltaTime);
            if (transform.GetComponent<CircleCollider2D>().OverlapCollider(WallFilter, Colliders) > 0)
            {
                WeaponRange = BaseWeaponRange * 3;
            }
            else if (transform.GetComponent<CircleCollider2D>().OverlapCollider(TurretFilter, Colliders) > 0)
            {
                WeaponRange = BaseWeaponRange * 1.5f;
            }
            else
            {
            }
            WeaponRange = BaseWeaponRange;
        }
        else
        {
            if (EnemyTarget == null || Vector3.Distance(transform.position, EnemyTarget.position) > WeaponRange || !EnemyTarget.gameObject.activeSelf)
            {
                FindNewTarget("Enemy");
            }
            if (EnemyTarget != null)
            {
                LookAtPosition(EnemyTarget.position);
                FireOnTarget(EnemyFilter);
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
