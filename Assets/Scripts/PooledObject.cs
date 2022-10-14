using System.Collections.Generic;
using UnityEngine;
using System;

public class PooledObject : MonoBehaviour
{

    private static void ThrowPoolAlreadySet()
    {
        throw new NotSupportedException("Pool object ownership cannot be changed.");
    }

    private PoolManager pool;
    public PoolManager ParentPool
    {
        get => pool;
        set
        {
            if (pool == null)
            {
                pool = value;
            }
            else
            {
                ThrowPoolAlreadySet();
            }
        }
    }

    public virtual void Return()
    {
        gameObject.transform.parent = pool.transform;
        gameObject.SetActive(false);
        pool.Return(this.gameObject);
    }

    public void OnDestroy()
    {
        if (!pool.isPoolShuttingDown)
            Debug.LogWarning("BUG!! Pooled object destroyed outside of object pool.");
    }
}