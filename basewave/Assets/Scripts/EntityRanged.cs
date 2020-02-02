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
                        proj.Reset(transform.position, transform.rotation, transform.right * ProjectileSpeed, WeaponRange*4*Time.fixedDeltaTime*Speed);
                        proj.ContactFilter = filter;
                        proj.Damage = Damage;
                        proj.Friendly = Friendly;
                    }


                    break;
                };
            
            case ClassType.RangedBeam:
                {
                    //if (TimeLastFired + (1.0f / FireRate) <= Time.realtimeSinceStartup) //Beams are continuous
                    //{
                        TimeLastFired = Time.realtimeSinceStartup;
                        /*
                        // Borrows a projectile from the Object Pool
                        var obj = Global.ProjectilePool.Rent();
                        obj.SetActive(true);
                        var proj = obj.GetComponent<Projectile>();
                        ParticleManager.EmitAt(ParticleManager.TheParticleManager.PlasmaShoot, this.transform.position, transform.right);
                        proj.Reset(transform.position, transform.rotation, transform.right * ProjectileSpeed, BaseWeaponRange / ProjectileSpeed);
                        proj.ContactFilter = filter;
                        proj.Damage = Damage;
                        proj.Friendly = this.Friendly;
                        */
                        
                        
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
                            } else
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
                    //}
                    break;
                }
            case ClassType.Shotgun:
                {
                    if (TimeLastFired + (1.0f / FireRate) <= Time.realtimeSinceStartup)
                    {
                        TimeLastFired = Time.realtimeSinceStartup;
                        ParticleManager.EmitAt(ParticleManager.TheParticleManager.ShotgunBlast, this.transform.position, transform.right);
                        Physics2D.OverlapCircle(new Vector2(this.transform.position.x, this.transform.position.y), WeaponRange, filter,colliderhitlist);
                        foreach (Collider2D col in colliderhitlist)
                        {

                            hitentity = col.GetComponent<Entity>();
                            if ((hitentity != null))
                            {
                                    Vector2 dif = new Vector2(col.transform.position.x, col.transform.position.y) - new Vector2(this.transform.position.x, this.transform.position.y);
                                    float mag = dif.magnitude;
                                    float mr = 1f - (mag / WeaponRange);
                                    float dot = Vector2.Dot(new Vector2(transform.right.x,transform.right.y).normalized,dif.normalized);
                                    if (dot > .25f) {
                                        ParticleManager.EmitAt(ParticleManager.TheParticleManager.ShotgunPelletImpact, col.transform.position, -dif.normalized);
                                        hitentity.Hit(Damage * mr);
                                    }
                                    
                                    
                                }
                            }
                            
                            

                        }
                        /*
                        // Borrows a projectile from the Object Pool
                        var obj = Global.ProjectilePool.Rent();
                        obj.SetActive(true);
                        var proj = obj.GetComponent<Projectile>();
                        
                        proj.Reset(transform.position, transform.rotation, transform.right * ProjectileSpeed, BaseWeaponRange / ProjectileSpeed);
                        proj.ContactFilter = filter;
                        proj.Damage = Damage;
                        proj.Friendly = this.Friendly;
                        */
                    }
                    break;
                }
        }
        
    }
