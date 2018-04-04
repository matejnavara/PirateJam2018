using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjectManager : MonoBehaviour {

    private Dictionary<string, ObjectPool> objectPoolMap;

    private static PoolObjectManager instance;
    public static PoolObjectManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PoolObjectManager>();
            }
            return instance;
        }
    }

    public ObjectPool GetPool( string name )
    {
        Debug.Assert(objectPoolMap.ContainsKey(name), "Asking for a pool that doesn't exist: " + name);
        return objectPoolMap[name];
    }

    void Awake () {
        objectPoolMap = new Dictionary<string, ObjectPool>();
        var pools = GetComponents<ObjectPool>();
        foreach( var pool in pools )
        {
            Debug.Assert(!objectPoolMap.ContainsKey(pool.poolName), "Two pools have the same name: " + pool.poolName);
            objectPoolMap.Add(pool.poolName, pool);
        }
	}
}
