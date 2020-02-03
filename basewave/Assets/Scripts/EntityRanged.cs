using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
    Entity Class for the Plasma Gun Unit
*/
public class EntityRanged : Entity
{
    private List<Collider2D> colliderhitlist = new List<Collider2D>();
    protected Entity hitentity;
    private RaycastHit2D[] raycasthitlist = new RaycastHit2D[32];
    protected LineRenderer MyBeamLineRenderer = null;
    protected ParticleSystem MyBeamEmitter = null;
    protected bool beamused = false;
    public override void FireOnTarget(ContactFilter2D filter, bool Friendly)
    {
        switch (Class)
        {
            case ClassType.RangedProjectile:
                {
                    if (TimeLastFired + (1.0f / FireRate) <= Time.realtimeSinceStartup)
                    {
                        TimeLastFired = Time.realtimeSinceStartup;
                        // Borrows a projectile from the Object Pool
                        var obj = Global.ProjectilePool.Rent();
                        obj.SetActive(true);
                        var proj = obj.GetComponent<Projectile>();
                        ParticleManager.EmitAt(ParticleManager.TheParticleManager.PlasmaShoot, this.transform.position, transform.right);

                        proj.Reset(transform.position, transform.rotation * Quaternion.Euler(0, 0, 8 * Random.value - 8 * Random.value), ProjectileSpeed, WeaponRange * 4 * Time.fixedDeltaTime * Speed, false);

                        proj.ContactFilter = filter;
                        proj.Damage = Damage;
                        proj.Friendly = Friendly;
                        proj.IsShotgunProjectile = false;
                        AudioManager.PlayLocalSound(AudioManager.Instance.PlasmaBoltSound);
                    }
                    break;
                };

            case ClassType.RangedBeam:
                {
                    TimeLastFired = Time.realtimeSinceStartup;

                    //ParticleManager.EmitAt(ParticleManager.TheParticleManager.ShotgunBlast, this.transform.position, transform.right);
                    if (MyBeamLineRenderer == null)
                    {
                        MyBeamLineRenderer = GameObject.Instantiate(ParticleManager.TheParticleManager.BeamLineRendererPrefab, this.transform.position, this.transform.rotation, this.transform);
                        MyBeamEmitter = GameObject.Instantiate(ParticleManager.TheParticleManager.BeamEmitterPrefab, this.transform.position, this.transform.rotation, this.transform);
                    }
                    beamused = true;

                    Vector3 hitpos = (this.transform.position + transform.right.normalized * WeaponRange);
                    int rc = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(transform.right.x, transform.right.y).normalized, filter, raycasthitlist, WeaponRange);
                    RaycastHit2D bestraycast = new RaycastHit2D();
                    bool hasbest = false;
                    float bestmag = 0f;
                    Entity besthitentity = null;
                    for (int i = 0; i < rc; i++)
                    {
                        RaycastHit2D rh = raycasthitlist[i];
                        Collider2D col = rh.collider;
                        if (col == null) continue;
                        hitentity = col.GetComponent<Entity>();
                        if ((hitentity != null))
                        {

                            if ((!hasbest) || (rh.distance < bestmag)) //Choose the closest target to hit on the Raycast
                            {
                                bestraycast = rh;
                                hasbest = true;
                                bestmag = rh.distance;
                                besthitentity = hitentity;
                            }
                        }
                        else
                        {
                            if ((!hasbest) || (rh.distance < bestmag)) //Choose the closest target to hit on the Raycast
                            {
                                bestraycast = rh;
                                hasbest = true;
                                bestmag = rh.distance;
                                hitentity = null;
                            }
                        }
                    }

                    if (hasbest)
                    {
                        hitpos = bestraycast.point;
                        if (hitentity != null)
                        {
                            hitentity.Hit(Damage * Time.fixedDeltaTime);
                            ParticleManager.EmitAt(ParticleManager.TheParticleManager.BeamSparks, 1, bestraycast.point, Quaternion.LookRotation(bestraycast.normal));
                        }
                    }

                    MyBeamLineRenderer.SetPositions(new Vector3[] { this.transform.position, hitpos });
                    MyBeamLineRenderer.enabled = true;
                    if (!MyBeamEmitter.isPlaying)
                        MyBeamEmitter.Play(true);

                    break;
                }
            case ClassType.Shotgun:
                {
                    if (TimeLastFired + (1.0f / FireRate) <= Time.realtimeSinceStartup)
                    {
                        TimeLastFired = Time.realtimeSinceStartup;

                        // Borrows a projectile from the Object Pool
                        ParticleManager.EmitAt(ParticleManager.TheParticleManager.ShotgunBlast, this.transform.position, transform.right);
                        int pelletcount = 7;
                        Vector3 dir = transform.right;
                        Vector3 crs = Vector3.Cross(transform.right, transform.forward);
                        float spreadangle = 35f;
                        for (float f = -1f; f <= 1f; f += (2f / (pelletcount - 1)))
                        {
                            var obj = Global.ProjectilePool.Rent();
                            obj.SetActive(true);
                            var proj = obj.GetComponent<Projectile>();
                            proj.transform.rotation = this.transform.rotation;
                            proj.transform.Rotate(0f, f * (spreadangle / 360f), 0f, Space.World);
                            proj.Reset(transform.position, proj.transform.rotation, ProjectileSpeed, WeaponRange * 4 * Time.fixedDeltaTime * Speed, true);
                            proj.ContactFilter = filter;
                            proj.Damage = Damage;
                            proj.Friendly = Friendly;

                        }
                    }
                }
                break;
        }
    }
}
