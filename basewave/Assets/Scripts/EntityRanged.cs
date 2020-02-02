

using UnityEngine;

public class EntityRanged : Entity
{
    public override void FireOnTarget(ContactFilter2D filter)
    {
        if (TimeLastFired + (1.0f / FireRate) <= Time.realtimeSinceStartup)
        {
            TimeLastFired = Time.realtimeSinceStartup;
            var obj = Global.ProjectilePool.Rent();
            if (obj == null)
                return;
            obj.SetActive(true);
            var proj = obj.GetComponent<Projectile>();
            ParticleManager.EmitAt(ParticleManager.TheParticleManager.PlasmaShoot, this.transform.position, transform.right);
            proj.Reset(transform.position, transform.rotation, transform.right * ProjectileSpeed, BaseWeaponRange / ProjectileSpeed);
            proj.ContactFilter = filter;
            proj.Damage = Damage;
        }
    }
}