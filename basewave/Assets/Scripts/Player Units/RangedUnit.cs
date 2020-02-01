using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedUnit : Entity
{

    // Use this for initialization
    void Start()
    {

    }

    public void FixedUpdate()
    {
        // Kill Test
        if (HitPoints <= 0)
        {
            Die();
            return;
        }

        // Attack Order
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
                WeaponRange = BaseWeaponRange;
        }
        else
        {
            if (EnemyTarget == null || Vector3.Distance(transform.position, EnemyTarget.position) > WeaponRange)
            {
                FindNewTarget("Enemy");
            }
            if (EnemyTarget != null)
            {
                FireOnTargetRanged();
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
