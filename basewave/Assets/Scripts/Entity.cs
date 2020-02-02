using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Could this not be an Abstract class?
    Logic Attributes:
    - {EntityClass, Position, Speed, HitPoints, IsDead}
    View Attributes:
    - {SpriteOffset, Death, Heal, Attack}
*/
public abstract class Entity : MonoBehaviour, IDamageable, IHealable {
    public Vector3 TargetPos;
    public float Speed;

    public Transform EnemyTarget;
    public Transform RangeCollider;
    public float Speed;
    private float HitPoints;
    public float Damage;
    public float FireRate;
    public float ProjectileSpeed;
    public float BaseWeaponRange;
    private float _WeaponRange;
    public float TimeLastFired;
    public float WeaponRange { get { return _WeaponRange; } set { RangeCollider.GetComponent<CircleCollider2D>().radius = value; _WeaponRange = value; } }
    public ClassType Class;


    /// <summary>
    ///  Used for collider checking this reused so we allocate less memory.
    /// </summary>
    /// <typeparam name="Collider2D"></typeparam>
    /// <returns></returns>
    protected List<Collider2D> Colliders = new List<Collider2D>();

    public bool IsDead {
        get { return (this.HitPoints <= 0); }
    }

    public float SpriteOffset;
    public bool AttackingBase = false;

    public void Die()
    {
        //Todo Particles;
        gameObject.SetActive(false);
    }

    public abstract void FireOnTarget(ContactFilter2D filter);

    public void Hit(float DamagePoints)
    {
        this.HitPoints -= DamagePoints;
    }

    public void Heal(float HitPoints)
    {
        this.HitPoints += HitPoints;
    }


    public void AttackBase()
    {
        AttackingBase = true;
        Global.Controller.Hit(Damage * Time.fixedDeltaTime);
    }

    public void LookAtPosition(Vector3 pos)
    {
        var dif = pos - transform.position;
        var sign = (pos.y < transform.position.y) ? -1.0f : 1.0f;
        transform.eulerAngles = new Vector3(0, 0, Vector3.Angle(Vector3.right, dif) + SpriteOffset) * sign;
    }

    public void FindNewTarget(string target) {
        EnemyTarget = null;
        Colliders.Clear();

        var x = new ContactFilter2D();
        x.SetLayerMask(LayerMask.GetMask(target));

        if (RangeCollider.GetComponent<CircleCollider2D>().OverlapCollider(x, Colliders) > 0) {
            var MaxDistance = WeaponRange + 1;
            Transform trans = null;

            foreach (var col in Colliders) {
                var distance = Vector2.Distance(col.transform.position, transform.position);
                if (distance < MaxDistance) {
                    MaxDistance = distance;
                    trans = col.transform;
                }
            }
            if (trans != null) {
                EnemyTarget = trans;
            }
        }
    }
}