using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PooledObject), typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    protected PooledObject poolLink;
    protected Rigidbody rb;

    private void Awake()
    {
        poolLink = GetComponent<PooledObject>();
        rb = GetComponent<Rigidbody>();
    }

    public void Reset(Vector3 position, Quaternion rotation, Vector3 velocity)
    {
        transform.position = position;
        transform.rotation = rotation;
        rb.velocity = velocity;
        rb.angularVelocity = new Vector3(0f, 0f, 0f);
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