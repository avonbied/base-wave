using UnityEngine;
using System;

public class SpawnerActivator : MonoBehaviour
{
    [SerializeField]
    private ProjectileSpawner spawner;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            spawner.Triggered = true;
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            spawner.Triggered = false;
        }
    }
}