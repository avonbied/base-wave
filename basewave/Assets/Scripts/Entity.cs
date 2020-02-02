using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable, IHealable
{

    public Vector3 TargetPos;
    public bool TargetPosDesignated = true;
    public Transform EnemyTarget;
    public Transform RangeCollider;
    public float Speed;
    [SerializeField]
    private float HitPoints;
    public float Damage;
    public float FireRate;
    public float ProjectileSpeed;
    public float BaseWeaponRange;
    private float _WeaponRange;
    public float TimeLastFired;

    public bool Friendly = false;

    public float CreditWorth;
    public float WeaponRange { get { return _WeaponRange; } set { RangeCollider.GetComponent<CircleCollider2D>().radius = value; _WeaponRange = value; } }
    public ClassType Class;

    /// <summary>
    ///  Used for collider checking this reused so we allocate less memory.
    /// </summary>
    /// <typeparam name="Collider2D"></typeparam>
    /// <returns></returns>
    protected List<Collider2D> Colliders = new List<Collider2D>();

    public bool IsDead
    {
        get { return (this.HitPoints <= 0); }
    }
    public float SpriteOffset;
    public bool AttackingBase = false;

    protected void Start()
    {
        if (Friendly)
        {
            Damage += 0.17f * Damage * (Time.timeSinceLevelLoad / 10);
        }
        else
        {
            HitPoints += 0.15f * HitPoints * (Time.timeSinceLevelLoad / 10);
        }
    }

    public void Die()
    {
        float points = 0;

        //Todo Particles;
        switch (Class)
        {
            case ClassType.Melee:
                points += 20;
                break;
            case ClassType.RangedBeam:
                points += 40;
                break;
            case ClassType.RangedMortar:
                points += 80;
                break;
            case ClassType.RangedProjectile:
                points += 20;
                break;
            case ClassType.Shotgun:
                points += 30;
                break;
            case ClassType.SuicideBomber:
                points += 20;
                break;
        }
        points -= 0.05f * points * (Time.timeSinceLevelLoad / 10);

        Global.Controller.Credits += points;

        Global.UnitSpawner.CurrentOnScreenCount -= 1;
        Destroy(gameObject);
    }

    public abstract void FireOnTarget(ContactFilter2D filter, bool Friendly);

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
        ParticleManager.EmitAt(ParticleManager.TheParticleManager.WallHit,1, transform.position+transform.right*.2f,new Quaternion());
        if (Class == ClassType.SuicideBomber)
        {
            Explode();
        }
    }



    public void Explode()
    {
        
    }

    public void LookAtPosition(Vector3 pos)
    {
        var dif = pos - transform.position;
        var sign = (pos.y < transform.position.y) ? -1.0f : 1.0f;
        var rot = Vector3.Angle(Vector3.right, dif) + SpriteOffset;
        if (rot < 0)
            rot += 360;
        else if (rot >= 360)
            rot -= 360;
        transform.eulerAngles = new Vector3(0, 0, rot) * sign;
    }

    public void FindNewTarget(string target)
    {
        EnemyTarget = null;
        Colliders.Clear();

        var x = new ContactFilter2D();
        x.SetLayerMask(LayerMask.GetMask(target));

        if (RangeCollider.GetComponent<CircleCollider2D>().OverlapCollider(x, Colliders) > 0)
        {
            var MaxDistance = WeaponRange + 1;
            Transform trans = null;

            foreach (var col in Colliders)
            {
                var distance = Vector2.Distance(col.transform.position, transform.position);
                if (distance < MaxDistance)
                {
                    MaxDistance = distance;
                    trans = col.transform;
                }
            }

            if (trans != null)
            {
                EnemyTarget = trans;
            }
        }
    }
}
