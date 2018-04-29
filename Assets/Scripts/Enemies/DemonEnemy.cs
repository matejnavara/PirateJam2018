using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonEnemy : Enemy {

    private GameManager gm;

    private Rigidbody2D rdb2;
    private Animator anim;

    public float smoothTime;
    public Vector3 smoothVelocity;
    private float walkAngle;
    private bool dying;

    private AudioSource audioSource;
    public AudioClip spawnSound;
    public AudioClip attackSound;
    public AudioClip hitSound;
    public AudioClip deathSound;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        rdb2 = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        moveRate = 3f;
        damageAmount = 6f;
        smoothTime = 1.3f;
        smoothVelocity = Vector3.zero;
        dying = false;
        anim.SetBool("alive", true);
        audioSource.PlayOneShot(spawnSound);
    }

    private void OnEnable()
    {       
        health.Respawn();
        //audioSource.PlayOneShot(spawnSound);
    }

    void FixedUpdate () {

        if (!gm.isGameOver())
        {
            if(health.GetCurrentHealth() > 0 && !dying)
            {
                Vector2 distance = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized;
                rdb2.AddForce(distance / moveRate, ForceMode2D.Impulse);
                //walkAngle = Vector2.Angle(distance, Vector2.zero);
                walkAngle = Vector2.SignedAngle(Vector2.up, distance);
                anim.SetFloat("walkAngle", walkAngle);
            }
            else if(!dying)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(deathSound);
                }
                anim.SetBool("alive", false);
                dying = true;
            }     
        }

          
	}

    override public void Hit(float amount, Vector2 direction)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(hitSound);
        }
        anim.SetTrigger("onHit");
        health.Damage(amount);
        rgdb.AddForce(direction * amount, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision between " + gameObject.name + " and " + col.gameObject.name + " with tag " + col.gameObject.tag);
        if (col.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(attackSound);
            anim.SetTrigger("onAttack");
            Vector2 force = (col.transform.position - transform.position).normalized;
            player.Hit(damageAmount, force * 5f);
        }
    }
}
