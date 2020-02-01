using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public Vector3 TargetPos;
    public Transform EnemyTarget;
    public Transform RangeCollider;
    public float Speed;
    public float HitPoints;
    public float Damage;
    public float FireRate;
    public float ProjectileSpeed;
    public float WeaponRange;
    public ClassType Class;
    public bool Dead;
    public float SpriteOffset;

	// Use this for initialization
	void Start () {
		
	}







    public void Die()
    {
        if (Dead)
            return;
        Dead = true;
        throw new NotImplementedException();
    }

    public void FireOnTargetRanged()
    {

    }

    public void LookAtPosition(Vector3 pos)
    {
        var dif = pos - transform.position;
        var sign = (pos.y < transform.position.y) ? -1.0f : 1.0f;
        transform.eulerAngles = new Vector3(0, 0, Vector3.Angle(Vector3.right, dif) + SpriteOffset) * sign;
    }
}
