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
                EnemyTarget = null;
                List<Collider2D> cols = new List<Collider2D>();
                Debug.Log(RangeCollider.GetComponent<CircleCollider2D>().OverlapCollider(new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.NameToLayer("Enemy") }, cols));
                if (cols.Count > 0)
                {
                    var MaxDistance = WeaponRange + 1;
                    Transform trans = null;
                    for (int i = 0; i<cols.Count; i++)
                    {
                        var distance = Vector2.Distance(cols[i].transform.position, transform.position);
                        if (distance < MaxDistance)
                        {
                            MaxDistance = distance;
                            trans = cols[i].transform;
                        }
                    }
                    if (trans != null)
                    {
                        EnemyTarget = trans;
                    }
                }
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
