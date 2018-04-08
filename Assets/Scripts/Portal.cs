using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    private Sprite[] portals;
    private Animator anim;

    private bool activated;
    private bool charging;
    public int level;

    private float spawnTime;
    private float spawnBase;

    public float timeToSpawn;

    private ObjectPool batPool;
    private ObjectPool skullPool;
    private ObjectPool demonPool;

    public Baby baby;
    private Progress chargeTimer;

    // Use this for initialization
    private void Start()
    {
        portals = Resources.LoadAll<Sprite>("Sprites/portals");
        anim = GetComponent<Animator>();
        batPool = PoolObjectManager.Instance.GetPool("BatPool");
        skullPool = PoolObjectManager.Instance.GetPool("SkullPool");
        demonPool = PoolObjectManager.Instance.GetPool("DemonPool");
        baby = GameObject.FindGameObjectWithTag("Baby").GetComponent<Baby>();
        chargeTimer = baby.GetComponentInChildren<Progress>();
    }

    void OnEnable()
    {
        NullCheck();
        activated = true;
        charging = true;
        level = 1;

        spawnBase = 130f;
        spawnTime = spawnBase;
        timeToSpawn = spawnTime;
        baby.ContactPortal(gameObject.name, true);
        anim.SetInteger("PortalLevel", level);
    }

    // Update is called once per frame
    void Update () {

        if(charging)
        {
            Charge();
        }

        if (activated)
        {
            Spawn();
        }
    }
    
       
    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            timeToSpawn = spawnTime;
            activated = false;
            baby.portalCount++;
            GetComponent<PooledObject>().ReturnObject();
        }

        if (col.gameObject.tag == "Baby" && !charging)
        {
            charging = true;
            baby.ContactPortal(gameObject.name, true);
        }

    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Baby" && charging)
        {
            charging = false;
            chargeTimer.StopTimer();
            baby.ContactPortal("", false);
        }
    }

    private void Charge()
    {
        if(level < 3)
        {
            if (!chargeTimer.timerStarted && !chargeTimer.complete)
            {
                chargeTimer.StartTimer(level * 2f);

            }
            else if (chargeTimer.complete)
            {
                level++;
                anim.SetInteger("PortalLevel", level);
                spawnTime = spawnBase * level;
                timeToSpawn = spawnTime / 2;
                activated = true;
                chargeTimer.complete = false;
            }
        }       
    }

    private void Spawn()
    {
        timeToSpawn -= 1f;

        if (timeToSpawn <= 0)
        {
            if(level == 1)
            {
                print("Spawning Bat");
                batPool.GetObject(transform.position);
                timeToSpawn = spawnTime;
            }

            if (level == 2)
            {
                print("Spawning Skull");
                skullPool.GetObject(transform.position);
                timeToSpawn = spawnTime;
            }

            if (level == 3)
            {
                print("Spawning Death");
                demonPool.GetObject(transform.position);
                timeToSpawn = spawnTime;
            }


        }
    }

    private void NullCheck()
    {
        if(portals == null){ portals = Resources.LoadAll<Sprite>("Sprites/portals"); }       
        //if(batPool == null) { batPool = PoolObjectManager.Instance.GetPool("BatPool"); }
        //if(skullPool == null) { skullPool = PoolObjectManager.Instance.GetPool("SkullPool"); }
        //if (demonPool == null) { demonPool = PoolObjectManager.Instance.GetPool("DemonPool"); }
        if(baby == null) { baby = GameObject.FindGameObjectWithTag("Baby").GetComponent<Baby>(); }
        if(chargeTimer == null) { chargeTimer = baby.GetComponentInChildren<Progress>(); }
        
    }
}
