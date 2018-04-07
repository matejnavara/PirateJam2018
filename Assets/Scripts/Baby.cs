using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class Baby : MonoBehaviour{
 // Normal Movements Variables

    private float baseSpeed;
    private float moveSpeed;
    private bool alive;
    public int portalCount;


    public Health health;
    public bool beingExorcised;
    public Rigidbody2D rbd2;

    public GameObject portal;
    private ObjectPool portalPool;
    public bool onPortal;
    public string contactPortal;
    


    void Start()
    {
        baseSpeed = 5f;
        moveSpeed = baseSpeed;
        alive = true;
        portalCount = 4;


        health = GetComponent<Health>();
        rbd2 = GetComponent<Rigidbody2D>();
        portalPool = PoolObjectManager.Instance.GetPool("PortalPool");

        onPortal = false;
        contactPortal = "";

    }

    void FixedUpdate()
    {

        if (health.GetCurrentHealth() > 0)
        {

            Summon();
            Exorcise();
            Move();

        }
        else
        {
            print("Dead");
        }

    }

    void Move()
    {
        if (onPortal)
        {
            moveSpeed = baseSpeed / 2;

        }
        else
        {
            moveSpeed = baseSpeed;
        }

        rbd2.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal2") * moveSpeed, 0.8f),
                                        Mathf.Lerp(0, Input.GetAxis("Vertical2") * moveSpeed, 0.8f));
    }

    void Summon()
    {

        if (Input.GetButton("Fire2a") && !onPortal && portalCount > 0)
        {
            portalPool.GetObject(transform.position);
            portalCount--;
        }

        if (Input.GetButton("Fire2b") && onPortal)
        {
            portalPool.ReturnObject(GameObject.Find(contactPortal).GetComponent<PooledObject>());
            portalCount++;
        }

    }

    public void ContactPortal(string name, bool foo)
    {
        contactPortal = name;
        onPortal = foo;
    }

    void Exorcise()
    {
        if (beingExorcised)
        {
            moveSpeed = baseSpeed / 2;
            health.Damage(1f);
            print("IT BURNS!");
        } else
        {
            moveSpeed = baseSpeed;
        }
    }

    public void Exorcism(bool foo)
    {
        beingExorcised = foo;
    }

}