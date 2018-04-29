﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

 public class Player : MonoBehaviour{

    private GameManager gm;

    private int playerId = 0;
    private Rewired.Player player;

    private float baseSpeed;
    private float moveSpeed;
    private Weapon weapon;

    private bool isAlive;
    private bool isAttacking;
    private bool isHit;
    private float hitDuration;
    private float hitDurationMax;

    public ReadyMeter specialTimer;
    public Health health;
    public Rigidbody2D rbd2;
    public SpriteRenderer sr;
    public Animator anim;

    private AudioSource audioSource;
    public AudioClip[] hitSounds;
    public AudioClip superSound;
    public AudioClip deathSound;

    void Start()
	{
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        rbd2 = GetComponent<Rigidbody2D> ();
        weapon = GetComponentInChildren<Weapon>();
        health = GetComponent<Health>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        player = ReInput.players.GetPlayer(playerId);
        PlayerReset();
    }

	void FixedUpdate()
	{
        if (!gm.isGameOver())
        {
            if(health.GetCurrentHealth() > 0)
            {

                Attack();
                Move();
         
            }
            else
            {
                if (!audioSource.isPlaying && isAlive)
                {
                    audioSource.PlayOneShot(deathSound);
                }
                isAlive = false;
            }
        }
 	}

    void Attack()
    {

        if (player.GetButton("Fire1"))
        {
            isAttacking = true;
            weapon.SetColliderActive(true);
            weapon.SwingSound();
            if (player.GetAxis("MoveHorizontal") >= 0)
            {
                weapon.transform.Rotate(0, 0, -20f);
            }
            else
            {
                weapon.transform.Rotate(0, 0, 20f);
            }
                
        }

        if (player.GetButtonUp("Fire1"))
        {
            isAttacking = false;
            weapon.SetColliderActive(false);
            weapon.transform.localRotation = Quaternion.Lerp(weapon.transform.rotation, Quaternion.identity, 1f);
        }

        if (player.GetButton("Fire2"))
        {
            Special();
        }
        
    }

    void Move()
    {
        if (isAttacking)
        {
            moveSpeed = baseSpeed / 3;
        }
        else
        {
            moveSpeed = baseSpeed;
        }


        if (!isHit)
        {
            rbd2.velocity = new Vector2(Mathf.Lerp(0, player.GetAxis("MoveHorizontal") * moveSpeed, 0.8f),
                                        Mathf.Lerp(0, player.GetAxis("MoveVertical") * moveSpeed, 0.8f));
        } else
        {
            rbd2.AddForce(new Vector2(player.GetAxis("MoveHorizontal") * moveSpeed, player.GetAxis("MoveVertical") * moveSpeed));
            hitDuration -= 1f;
            if (hitDuration < 0) { isHit = false; }
        }
        

        if(player.GetAxis("MoveHorizontal") >= 0)
        {
            sr.flipX = false;
            sr.sortingOrder = 51;
            //weapon.gameObject.transform.localPosition.Set(0.17f, -0.13f, 0);
        } else
        {
            sr.flipX = true;
            sr.sortingOrder = 50;
        }
    }

    public void Hit(float amount, Vector2 direction)
    {
        if (!audioSource.isPlaying)
        {
            int index = Random.Range(0, hitSounds.Length);
            audioSource.clip = hitSounds[index];
            audioSource.Play();
        }
        anim.SetTrigger("onHit");
        isHit = true;
        hitDuration = hitDurationMax;
        health.Damage(amount);
        rbd2.AddForce(direction * amount, ForceMode2D.Impulse);
    }

    void Special()
    {
        if (specialTimer.ready && !isAttacking)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(superSound);
            }
            anim.SetTrigger("onSuper");           
        } else
        {
            //still charging notification
        }
    }

    public void FireSpecial()
    {
        Vector3 pushPos = transform.position;
        float radius = 3f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pushPos, radius);

        foreach (Collider2D hit in colliders)
        {
            if (hit.tag == "Baby")
            {
                Vector2 force = (hit.transform.position - pushPos).normalized;
                hit.GetComponent<Baby>().Hit(force * 5f);
            }
            else if (hit.GetComponent<Rigidbody2D>() != null)
            {
                Vector2 force = (hit.transform.position - pushPos).normalized;
                hit.GetComponent<Rigidbody2D>().AddForce(force * 6f, ForceMode2D.Impulse);
                Debug.Log("KICKING " + hit.name + " with " + force);
            }
        }
        specialTimer.StartTimer(3f);
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    public bool IsHit()
    {
        return isHit;
    }

    public void PlayerReset()
    {
        isAlive = true;
        isAttacking = false;
        isHit = false;
        hitDurationMax = 10f;
        baseSpeed = 2f;
        moveSpeed = baseSpeed;
    }

 }