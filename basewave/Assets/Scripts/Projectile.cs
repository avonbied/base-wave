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
    public ContactFilter2D ContactFilter { get; set; }
    private List<Collider2D> Colliders = new List<Collider2D>();

    private void Awake()
    {
        poolLink = GetComponent<PooledObject>();
        rigidBody = GetComponent<Rigidbody2D>();
        projectileCollider = GetComponent<Collider2D>();
    }

    public virtual void Reset(Vector2 position, Quaternion rotation, Vector2 velocity, float flightTime)
    {
        this.flightTime = flightTime;
        transform.position = position;
        transform.rotation = rotation;
        rigidBody.velocity = velocity;
        rigidBody.angularVelocity = 0.0f;
        transform.parent = null;
    }

    protected virtual void FixedUpdate()
    {
        flightTime -= Time.fixedDeltaTime;
        if (flightTime <= 0.0f)
        {
            poolLink.Return();
        }

        projectileCollider.OverlapCollider(ContactFilter, Colliders);

    }
}