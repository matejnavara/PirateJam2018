using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    private Sprite[] portals;
    private SpriteRenderer sr;

    private bool activated;
    private bool charging;
    public float charge;
    private float chargeRate;
    private int level;

    private float spawnTime;
    private float spawnBase;

    public float timeToSpawn;

    private ObjectPool batPool;
    private ObjectPool skullPool;
    private ObjectPool demonPool;

    // Use this for initialization
    void Start () {
        portals = Resources.LoadAll<Sprite>("Sprites/portals");
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = portals[0];
        batPool = PoolObjectManager.Instance.GetPool("BatPool");
        skullPool = PoolObjectManager.Instance.GetPool("SkullPool");
        demonPool = PoolObjectManager.Instance.GetPool("DemonPool");

        activated = false;
        charging = false;
        charge = 0;
        chargeRate = 1f;
        level = 0;

        spawnBase = 75f;
        spawnTime = spawnBase;
        timeToSpawn = spawnTime;
	}
	
	// Update is called once per frame
	void Update () {

        if(charging && charge <= 400)
        {
            Charge();
        }

        if (activated)
        {
            Spawn();
        }
    }

    //private void OnTriggerStay2D(Collider2D col)
    //{
    //    print("Detecting collision stay");
    //    if (col.gameObject.tag == "Baby" && charge <= 300)
    //    {
    //        Charge();
    //        print("Inside loop");
//        }
//    }

    
       
    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player" && activated)
        {
            timeToSpawn = spawnTime;
            charge = 0;
            level = 0;
            activated = false;
            sr.sprite = portals[0];
        }

        if (col.gameObject.tag == "Baby" && !charging)
        {
            charging = true;
        }

    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Baby" && charging)
        {
            charging = false;
            charge = level * 100;
        }
    }

    private void Charge()
    {

        charge += chargeRate;      

        if(charge > 100 && charge < 199 && level != 1)
        {
            level = 1;
            sr.sprite = portals[1];
            spawnTime = spawnBase;
            timeToSpawn = spawnTime;
            activated = true;
        }

        if (charge > 200 && charge < 399 && level != 2)
        {
            level = 2;
            sr.sprite = portals[2];
            spawnTime = spawnBase * 3;
            timeToSpawn = spawnTime;
        }

        if (charge >= 400 && level != 3)
        {
            level = 3;
            sr.sprite = portals[3];
            spawnTime = spawnBase * 5;
            timeToSpawn = spawnTime;
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
}
