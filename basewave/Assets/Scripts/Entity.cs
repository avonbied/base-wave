using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public Vector3 TargetPos;
    public Transform EnemyTarget;
    public float Speed;
    public float HitPoints;
    public float Damage;
    public float FireRate;
    public float ProjectileSpeed;
    public float WeaponRange;
    public ClassType Class;
    public bool Dead;

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
}
