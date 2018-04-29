using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullEnemy : Enemy {

    private GameManager gm;

    public float smoothTime;
    public Vector3 smoothVelocity;
    private Animator anim;
    private bool dying;

    private AudioSource audioSource;
    public AudioClip spawnSound;
    public AudioClip attackSound;
    public AudioClip hitSound;
    public AudioClip deathSound;

    void Start () {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        moveRate = 20f;
        damageAmount = 3f;
        dying = false;
        smoothTime = 2.0f;
        smoothVelocity = Vector3.zero;
        audioSource.PlayOneShot(spawnSound);
    }

    private void OnEnable()
    {
        dying = false;
        health.Respawn();
        //audioSource.PlayOneShot(spawnSound);
    }

    void FixedUpdate ()
    {
        if (!gm.isGameOver())
        {
            if(health.GetCurrentHealth() > 0)
            {
                //transform.LookAt(player.transform);
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance < moveRate)
                {
                    transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref smoothVelocity, smoothTime);
                }
            }
            else if(!dying)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(deathSound);
                }
                anim.SetTrigger("onDeath");
                dying = true;
            }
        }

        
        
	}

    override public void Hit(float amount, Vector2 direction)
    {
        audioSource.PlayOneShot(hitSound);
        anim.SetTrigger("onHit");
        health.Damage(amount);
        rgdb.AddForce(direction * amount, ForceMode2D.Impulse);        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(attackSound);
            player.Hit(damageAmount, Vector2.zero);
        }
    }

}
