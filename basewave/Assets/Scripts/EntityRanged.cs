

using UnityEngine;

public class EntityRanged : Entity
{
    public override void FireOnTarget(ContactFilter2D filter)
    {
        if (TimeLastFired + (1.0f / FireRate) <= Time.realtimeSinceStartup)
        {
            TimeLastFired = Time.realtimeSinceStartup;
            var obj = Global.ProjectilePool.Rent();
            obj.SetActive(true);
            var proj = obj.GetComponent<Projectile>();
            proj.Reset(transform.position, transform.rotation, transform.right * ProjectileSpeed, BaseWeaponRange / ProjectileSpeed);
            proj.ContactFilter = filter;
            proj.Damage = Damage;
        }
    }
}