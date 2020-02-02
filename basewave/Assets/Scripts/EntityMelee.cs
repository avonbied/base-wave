using UnityEngine;

public class EntityMelee : Entity
{
    public override void FireOnTarget(ContactFilter2D filter)
    {
        if (TimeLastFired + (1.0f / FireRate) <= Time.realtimeSinceStartup)
        {
            TimeLastFired = Time.realtimeSinceStartup;
            // Borrows a projectile from the Object Pool
            var obj = Global.ProjectilePool.Rent();
            if (obj == null)
                return;
            obj.SetActive(true);
            var proj = obj.GetComponent<Projectile>();
            ParticleManager.EmitAt(ParticleManager.TheParticleManager.MeleeHitEffect, EnemyTarget.position, -transform.right);
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
}