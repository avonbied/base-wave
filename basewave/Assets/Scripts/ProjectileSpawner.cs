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

    private ContactFilter2D enemyFilter;
    private ContactFilter2D friendlyFilter;

    Entity entity;

    private void Awake()
    {
        entity = GetComponent<Entity>();
        enemyFilter = new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.GetMask("Enemy") };
        enemyFilter = new ContactFilter2D() { useLayerMask = true, layerMask = LayerMask.GetMask("Friendly") };
    }

    public void Fire(bool Friendly) { 
            var obj = objectPool.Rent();
            obj.SetActive(true);
            var proj = obj.GetComponent<Projectile>();
            Debug.Log(entity.WeaponRange / entity.ProjectileSpeed);
            proj.Reset(transform.position, transform.rotation, transform.right * entity.ProjectileSpeed, entity.BaseWeaponRange / entity.ProjectileSpeed,false);
            proj.ContactFilter = Friendly?friendlyFilter:enemyFilter;
    }
}