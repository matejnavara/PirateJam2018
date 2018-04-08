using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

 public class Player : MonoBehaviour{

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


    void Start()
	{
        baseSpeed = 3f;
        rbd2 = GetComponent<Rigidbody2D> ();
        isAlive = true;
        isAttacking = false;
        isHit = false;
        hitDurationMax = 10f;
        weapon = GetComponentInChildren<Weapon>();
        health = GetComponent<Health>();
        sr = GetComponent<SpriteRenderer>();

        player = ReInput.players.GetPlayer(playerId);

        moveSpeed = baseSpeed;

    }

	void FixedUpdate()
	{
        
        if(health.GetCurrentHealth() > 0)
        {

            Attack();
            Move();
         
        }
        else
        {
            isAlive = false;
            print("Dead");
        }

 	}

    void Attack()
    {

        if (player.GetButton("Fire1"))
        {
            isAttacking = true;
            weapon.SetColliderActive(true);
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
        //anim.SetTrigger("onHit");
        isHit = true;
        hitDuration = hitDurationMax;
        health.Damage(amount);
        rbd2.AddForce(direction * amount, ForceMode2D.Impulse);
    }

    void Special()
    {
        if (specialTimer.ready && !isAttacking)
        {
            Vector3 pushPos = transform.position;
            float radius = 3f;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(pushPos, radius);

            foreach (Collider2D hit in colliders)
            {
                if(hit.tag == "Baby")
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
            specialTimer.StartTimer(5f);
        } else
        {
            //still charging notification
        }
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

 }