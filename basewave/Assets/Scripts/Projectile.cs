using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PooledObject), typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    protected PooledObject poolLink;
    protected Rigidbody2D rigidBody;

    private void Awake()
    {
        poolLink = GetComponent<PooledObject>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Reset(Vector2 position, Quaternion rotation, Vector2 velocity)
    {
        transform.position = position;
        transform.rotation = rotation;
        rigidBody.velocity = velocity;
        rigidBody.angularVelocity = 0.0f;
        transform.parent = null;
        StartCoroutine(TravelTimeKill());
    }

    // TODO - Nuke the coruotine!!!
    protected IEnumerator TravelTimeKill()
    {
        yield return new WaitForSeconds(0.4f);
        poolLink.Return();
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     poolLink.Return();
    // }
}