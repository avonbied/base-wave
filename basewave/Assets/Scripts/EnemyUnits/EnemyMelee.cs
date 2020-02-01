using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Entity {

	// Use this for initialization
	void Start () {
		
	}

	private void FixedUpdate()
	{
        // Kill Test
        if (this.Dead) {
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
            if (Vector3.Distance(transform.position, EnemyTarget.position) < WeaponRange * .4f)
            {
                FireOnTargetRanged();
                LookAtPosition(EnemyTarget.position);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, EnemyTarget.position, Speed * Time.fixedDeltaTime);
                LookAtPosition(EnemyTarget.position);
                if (transform.GetComponent<CircleCollider2D>().OverlapCollider(new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.GetMask("Turret") }, new List<Collider2D>()) > 0 || transform.GetComponent<CircleCollider2D>().OverlapCollider(new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.GetMask("Wall") }, new List<Collider2D>()) > 0)
                {
                    AttackBase();
                }
            }
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
}
