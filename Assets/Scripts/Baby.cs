using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

 public class Baby : MonoBehaviour{
    // Normal Movements Variables
    private int playerId = 1;
    private Rewired.Player player;

    private float baseSpeed;
    private float moveSpeed;
    public int portalCount;

    private bool isAlive;
    private bool isHit;
    private float hitDuration;
    private float hitDurationMax;

    public Health health;
    public bool beingExorcised;
    public Rigidbody2D rbd2;

    public GameObject portal;
    private ObjectPool portalPool;
    public bool onPortal;
    public string contactPortal;
    


    void Start()
    {
        player = ReInput.players.GetPlayer(playerId);

        baseSpeed = 3f;
        moveSpeed = baseSpeed;
        isAlive = true;
        isHit = false;
        portalCount = 4;
        hitDurationMax = 20f;

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
            isAlive = false;
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

        if (!isHit)
        {
            rbd2.velocity = new Vector2(Mathf.Lerp(0, player.GetAxis("MoveHorizontal") * moveSpeed, 0.8f),
                                        Mathf.Lerp(0, player.GetAxis("MoveVertical") * moveSpeed, 0.8f));
        }
        else
        {
            rbd2.AddForce(new Vector2(player.GetAxis("MoveHorizontal") * moveSpeed, player.GetAxis("MoveVertical") * moveSpeed));
            hitDuration -= 1f;
            if(hitDuration < 0) { isHit = false; }
        }
    }

    public void Hit(Vector2 force)
    {
        isHit = true;
        GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        hitDuration = hitDurationMax;
    }

    void Summon()
    {

        if (player.GetButton("Fire1") && !onPortal && portalCount > 0)
        {
            portalPool.GetObject(transform.position);
            portalCount--;
        }

        if (player.GetButton("Fire2") && onPortal)
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
            moveSpeed = baseSpeed / 1.5f;
            health.Damage(0.75f);
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

    public bool IsAlive()
    {
        return isAlive;
    }

}