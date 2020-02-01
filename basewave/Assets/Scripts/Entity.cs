using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Could this not be an Abstract class?
    Logic Attributes:
    - {EntityClass, Position, Speed, HitPoints, IsDead}
    View Attributes:
    - {SpriteOffset}
*/
public abstract class Entity : MonoBehaviour {

    public Vector3 TargetPos;
    public Transform EnemyTarget;
    public Transform RangeCollider;
    public float Speed;
    private float HitPoints;
    public float Damage;
    public float FireRate;
    public float ProjectileSpeed;
    public float BaseWeaponRange;
    private float _WeaponRange;
    public float WeaponRange { get { return _WeaponRange; } set { RangeCollider.GetComponent<CircleCollider2D>().radius = value; _WeaponRange = value; } }
    public ClassType Class;
    public float SpriteOffset;
    public bool AttackingBase = false;

	// Use this for initialization
	void Start () {
		
	}

    public bool Dead {
        get { return (this.HitPoints <= 0); }
        //private set {}
    }

    public void Hit(float HP) {
        HitPoints -= HP;
        //Todo Blood and Gore
    }

    // Could this be a support method for a Kill Test?
    public void Die() {
        throw new NotImplementedException();
    }

    public void FireOnTargetRanged() {
    }


    public void AttackBase()
    {
        AttackingBase = true;
    }

    public void LookAtPosition(Vector3 pos)
    {
        var dif = pos - transform.position;
        var sign = (pos.y < transform.position.y) ? -1.0f : 1.0f;
        transform.eulerAngles = new Vector3(0, 0, Vector3.Angle(Vector3.right, dif) + SpriteOffset) * sign;
    }

    public void FindNewTarget(string target)
    {
        EnemyTarget = null;
        List<Collider2D> cols = new List<Collider2D>();
        var x = new ContactFilter2D();
        x.SetLayerMask(LayerMask.GetMask(target));
        RangeCollider.GetComponent<CircleCollider2D>().OverlapCollider(x, cols);
        if (cols.Count > 0)
        {
            var MaxDistance = WeaponRange + 1;
            Transform trans = null;
            for (int i = 0; i < cols.Count; i++)
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
}
