using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Entity))]
public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectle;
    public bool Triggered { get; set; }

    [SerializeField]
    private PoolManager objectPool;

    private Coroutine cor;
    private float vel = 0;

    Entity entity;

    private void Awake()
    {
        entity = GetComponent<Entity>();
    }

    protected IEnumerator FireConsistantly()
    {
        while (Triggered)
        {
            var obj = objectPool.Rent();
            if (obj == null)
                yield break;
            obj.SetActive(true);
            obj.GetComponent<Projectile>().Reset(transform.position, transform.rotation, transform.right * entity.ProjectileSpeed);

            yield return new WaitForSeconds(entity.FireRate);
        }
    }

    protected virtual void Update()
    {
        if (Triggered && cor == null)
        {
            cor = StartCoroutine(FireConsistantly());
        }
        else if (!Triggered && cor != null)
        {
            StopCoroutine(cor);
            cor = null;
        }
    }
}