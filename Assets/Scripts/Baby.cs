using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

 public class Baby : MonoBehaviour{

    private GameManager gm;

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

    private AudioSource audioSource;
    public AudioClip spawnSound;
    public AudioClip hitSound;
    public AudioClip hurtSound;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = ReInput.players.GetPlayer(playerId);
        health = GetComponent<Health>();
        rbd2 = GetComponent<Rigidbody2D>();
        portalPool = PoolObjectManager.Instance.GetPool("PortalPool");
        audioSource = GetComponent<AudioSource>();
        BabyReset();
    }

    void FixedUpdate()
    {
        if (!gm.isGameOver())
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
            }
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
        audioSource.PlayOneShot(hitSound);
    }

    void Summon()
    {
        if (player.GetButton("Fire1") && !onPortal && portalCount > 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(spawnSound);
            }
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
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(hurtSound);
            }
            moveSpeed = baseSpeed / 1.5f;
            health.Damage(0.75f);
        } else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
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

    public void BabyReset()
    {
        baseSpeed = 4f;
        moveSpeed = baseSpeed;
        isAlive = true;
        isHit = false;
        portalCount = 4;
        hitDurationMax = 20f;
        onPortal = false;
        contactPortal = "";
    }

}