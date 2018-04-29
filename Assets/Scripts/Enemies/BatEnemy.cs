using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : Enemy {

    private GameManager gm;
    private Animator anim;

    private AudioSource audioSource;
    public AudioClip spawnSound;
    public AudioClip attackSound;
    public AudioClip deathSound;

    void Start () {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        moveRate = 20f;
        damageAmount = 10f;
        audioSource.PlayOneShot(spawnSound);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!gm.isGameOver())
        {
            if(health.GetCurrentHealth() > 0)
            {
                Vector2 heading = this.transform.position - player.transform.position;
                rgdb.AddForce(-heading * moveRate * Time.deltaTime);
            }
            else
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(deathSound);
                }           
                anim.SetTrigger("onDeath");
            }
        }
        
        
	}

    override public void Hit(float amount, Vector2 direction)
    {
        health.Damage(amount);
        rgdb.AddForce(direction * (amount/2), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(attackSound);
            player.Hit(damageAmount, Vector2.zero);
        }
    }
}
