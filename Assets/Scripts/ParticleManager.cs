using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    //Put all of the Particle Effects in the Particle Manager (with some possible exceptions)
    public static ParticleManager TheParticleManager;
    public ParticleSystem WallHit,WallDestructionEffect,ShotgunBlast,ShotgunPelletImpact,BomberExplosion, PlasmaShoot,PlasmaImpact, MeleeHitEffect,RepairEffect;
    public ParticleSystem BeamSparks;
    public ParticleSystem BeamEmitterPrefab;
    public LineRenderer BeamLineRendererPrefab;

    void Start()
    {
        TheParticleManager = this;
    }


    public static void EmitAt(ParticleSystem particlesystem,Vector3 location)
    {
        EmitAt(particlesystem, location, particlesystem.transform.rotation);
    }
    public static void EmitAt(ParticleSystem particlesystem, Vector3 location,Vector3 direction)
    {
        EmitAt(particlesystem, location, Quaternion.LookRotation(direction.normalized));
    }
    public static void EmitAt(ParticleSystem particlesystem, Vector3 location, Quaternion rotation)
    {
        if (particlesystem == null) return;
        particlesystem.transform.position = location;
        particlesystem.transform.rotation = rotation;

        foreach (ParticleSystem ps in particlesystem.gameObject.GetComponentsInChildren<ParticleSystem>())
        {
            if (ps == null) continue;
            ParticleSystem.EmissionModule em = particlesystem.emission;
            ps.Emit((em.GetBurst(0).count.mode == ParticleSystemCurveMode.TwoConstants) ?Random.Range(em.GetBurst(0).minCount, em.GetBurst(0).maxCount): ((int)em.GetBurst(0).count.constant));

        }
    }
    public static void EmitAt(ParticleSystem particlesystem,int particlecount,Vector3 location, Quaternion rotation)
    {
        if (particlesystem == null) return;
        particlesystem.transform.position = location;
        particlesystem.transform.rotation = rotation;

        foreach (ParticleSystem ps in particlesystem.gameObject.GetComponentsInChildren<ParticleSystem>())
        {
            if (ps == null) continue;
            ParticleSystem.EmissionModule em = particlesystem.emission;
            ps.Emit(particlecount);//ps.Emit((em.GetBurst(0).count.mode == ParticleSystemCurveMode.TwoConstants) ? Random.Range(em.GetBurst(0).minCount, em.GetBurst(0).maxCount) : ((int)em.GetBurst(0).count.constant));

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
