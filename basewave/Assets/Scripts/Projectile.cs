using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PooledObject), typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    protected PooledObject poolLink;
    protected Rigidbody2D rigidBody;
    protected Collider2D projectileCollider;
    protected float flightTime;
    public float Damage;
    public bool Friendly = false;
    public ContactFilter2D ContactFilter { get; set; }
    private List<Collider2D> Colliders = new List<Collider2D>();
    public bool Live = true;
    public TrailRenderer MyTrailRenderer;

    private void Awake()
    {
        poolLink = GetComponent<PooledObject>();
        rigidBody = GetComponent<Rigidbody2D>();
        projectileCollider = GetComponent<Collider2D>();
        if (MyTrailRenderer)
            MyTrailRenderer.Clear();
    }

    public virtual void Reset(Vector2 position, Quaternion rotation, float velocity, float flightTime)
    {
        this.flightTime = flightTime;
        transform.position = position;
        transform.rotation = rotation;
        rigidBody.velocity = transform.right*velocity;
        rigidBody.angularVelocity = 0.0f;
        transform.parent = null;
        if (MyTrailRenderer)
            MyTrailRenderer.Clear();
        Live = true;
    }

    protected virtual void FixedUpdate()
    {
        flightTime -= Time.fixedDeltaTime;
        if ((flightTime <= 0.0f) || (!Live))
        {
            Live = false;
            ParticleManager.EmitAt(ParticleManager.TheParticleManager.PlasmaImpact, this.transform.position);
            poolLink.Return();
        }

        projectileCollider.OverlapCollider(ContactFilter, Colliders);
        if (Colliders.Count > 0)
        {
            Colliders[0].GetComponent<Entity>().Hit(Damage);
            poolLink.Return();
        }
    }

    // private void OnTriggerEnter2D(Collider2D collision) //Marcus: "Projectiles had triggers in the first place. Might as well use them."
    // {
    //     if (!Live && collision == null)
    //         return;

    //     Entity hitentity = collision.GetComponent<Entity>();

    //     if (hitentity == null)
    //         return;

    //     if (this.Friendly != hitentity.Friendly)
    //     {
    //         ParticleManager.EmitAt(ParticleManager.TheParticleManager.PlasmaImpact, this.transform.position);
    //         hitentity.Hit(Damage);
    //         Live = false;
    //         poolLink.Return();
    //     }
    // }
}