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

    private ContactFilter2D commonFilter;

    Entity entity;

    private void Awake()
    {
        entity = GetComponent<Entity>();
        commonFilter = new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.GetMask("Enemy") };
    }

    protected IEnumerator FireConsistantly()
    {
        while (Triggered)
        {
            var obj = objectPool.Rent();
            if (obj == null)
                yield break;
            obj.SetActive(true);
            var proj = obj.GetComponent<Projectile>();
            Debug.Log(entity.WeaponRange / entity.ProjectileSpeed);
            proj.Reset(transform.position, transform.rotation, transform.right * entity.ProjectileSpeed, entity.BaseWeaponRange / entity.ProjectileSpeed);
            proj.ContactFilter = commonFilter;
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