using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolManager : MonoBehaviour
{
    public int MaxPoolSize = 512;

    protected static void ThrowInvalidPool()
    {
        throw new InvalidOperationException("Object returned to incorrect pool.");
    }

    [SerializeField]
    protected GameObject referenceObject;

    protected Stack<GameObject> pool;

    public int PooledObjectCount { get; protected set; }
    public bool isPoolShuttingDown { get; private set; }

    private void Awake()
    {
        pool = new Stack<GameObject>(MaxPoolSize);
    }

    public GameObject Rent()
    {
        if (pool.Count > 0)
        {
            return pool.Pop();
        }
        else
        {
            if (PooledObjectCount < MaxPoolSize)
            {
                PooledObjectCount++;
                var obj = Instantiate(referenceObject, transform);
                obj.GetComponent<PooledObject>().ParentPool = this;
                return obj;
            }
            else
            {
                return null;
            }
        }
    }

    public void Return(GameObject obj)
    {
        var poolFound = obj.GetComponent<PooledObject>();
        if (poolFound.ParentPool != this)
        {
            ThrowInvalidPool();
        }

        if (PooledObjectCount <= MaxPoolSize)
        {
            pool.Push(obj);
        }
        else
        {
            isPoolShuttingDown = true;
            Destroy(obj);
            isPoolShuttingDown = false;
        }
    }


    private void OnApplicationQuit()
    {
        isPoolShuttingDown = true;
    }
}