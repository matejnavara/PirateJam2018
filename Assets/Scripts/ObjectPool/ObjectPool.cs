using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour {

    public string poolName = "ObjectPool";
    public GameObject objectPrefab;
    public int poolSize;
    public float timeToLive = 30;
    public bool returnOnCollision = true;
    public bool returnOnTrigger = true;

    private PooledObject[] pool;
    private int poolIndex;
    private static ObjectPool instance;

    private void Awake()
    {
        pool = new PooledObject[poolSize];

        // Pre-populate the pool
        for( int i=0; i<poolSize; i++ )
        {
            // Create a new GameObject
            var poolGO = Instantiate(objectPrefab, transform);
            poolGO.name = "PooledObject_" + poolName + ":" + i.ToString();

            // Add a PoolObject Component, it takes care of return the object to the pool
            var pooledObject = poolGO.AddComponent<PooledObject>();
            pooledObject.owningPool = this;
            pooledObject.timeToLive = timeToLive;
            pooledObject.returnOnCollision = returnOnCollision;
            pooledObject.returnOnTrigger = returnOnTrigger;
            pool[i] = pooledObject;

            DeactivateObject(poolGO);
        }

        // Our pool is full, set the index.
        poolIndex = poolSize - 1;
    }


    public GameObject GetObject(Vector3 pos)
    {
        Debug.Log("GetObject: " + poolIndex);
        // Take an object out of the pool just by decrementing the pool index.
        Debug.Assert(poolIndex != 0, "Pool size exceeded");
        var pooledObject = pool[poolIndex];
        poolIndex--;
        return ActivateObject( pooledObject , pos);
    }

    public void ReturnObject( PooledObject returnedObject )
    {
        poolIndex++;
        Debug.Log("ReturnObject: " + poolIndex);
        Debug.Assert(poolIndex < poolSize, "To many objects returned to the pool.");
        pool[poolIndex] = returnedObject;
        DeactivateObject( returnedObject.gameObject );
    }

    private GameObject DeactivateObject( GameObject objectToDeactivate)
    {
        objectToDeactivate.SetActive(false);
        return objectToDeactivate;
    }

    private GameObject ActivateObject(PooledObject pooledObject, Vector3 pos)
    {
        pooledObject.StartFromPool();
        pooledObject.gameObject.transform.position = pos;
        pooledObject.gameObject.SetActive(true);
        return pooledObject.gameObject;
    }



}
