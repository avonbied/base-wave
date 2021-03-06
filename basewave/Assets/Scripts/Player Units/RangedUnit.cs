﻿using System.Collections;
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
        if (HitPoints <= 0)
        {
            Die();
            return;
        }


        if (Vector3.Distance(transform.position, TargetPos) >= 0.1)
        {
            LookAtPosition(TargetPos);
            transform.position = Vector3.MoveTowards(transform.position, TargetPos, Speed * Time.fixedDeltaTime);
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
