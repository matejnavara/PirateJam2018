using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour {

    public ObjectPool owningPool;
    public float timeToLive;
    public bool returnOnCollision;
    public bool returnOnTrigger;

    public void StartFromPool()
    {
        if (timeToLive > 0)
        {
            Invoke("ReturnObject", timeToLive);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (returnOnCollision) ReturnObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (returnOnTrigger) ReturnObject();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (returnOnCollision) ReturnObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (returnOnTrigger) ReturnObject();
    }

    public void ReturnObject()
    {
        CancelInvoke();
        owningPool.ReturnObject(this);
    }

}
