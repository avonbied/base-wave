using UnityEngine;

/*
    Entity Class for the Plasma Gun Unit
*/
public class EntityRanged : Entity
{
    public override void FireOnTarget(ContactFilter2D filter)
    {
        if (TimeLastFired + (1.0f / FireRate) <= Time.realtimeSinceStartup)
        {
            TimeLastFired = Time.realtimeSinceStartup;
            // Borrows a projectile from the Object Pool
            var obj = Global.ProjectilePool.Rent();
            obj.SetActive(true);
            var proj = obj.GetComponent<Projectile>();
            ParticleManager.EmitAt(ParticleManager.TheParticleManager.PlasmaShoot, this.transform.position, transform.right);
            proj.Reset(transform.position, transform.rotation, transform.right * ProjectileSpeed, BaseWeaponRange / ProjectileSpeed);
            proj.ContactFilter = filter;
            proj.Damage = Damage;
            proj.Friendly = this.Friendly;
        }
    }
}