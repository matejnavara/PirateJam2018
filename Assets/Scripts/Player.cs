using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class Player : MonoBehaviour{
	
	private float baseSpeed;
    private float movespeed;
    private Rigidbody2D rbd2;
    private bool alive;
    private Weapon weapon;
    private bool attacking;

    public Health health;


	void Start()
	{
        baseSpeed = 4f;
        rbd2 = GetComponent<Rigidbody2D> ();
        alive = true;
        weapon = GetComponentInChildren<Weapon>();
        health = GetComponent<Health>();

        movespeed = baseSpeed;

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
            print("Dead");
        }

 	}

    void Attack()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            attacking = true;
            weapon.SetColliderActive(true);
            weapon.transform.Rotate(0, 0, -20f);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            attacking = false;
            weapon.SetColliderActive(false);
            weapon.transform.localRotation = Quaternion.Slerp(weapon.transform.rotation, Quaternion.identity, 1f);
        }
        
    }

    public bool IsAttacking()
    {
        return attacking;
    }

    void Move()
    {
        if (attacking)
        {
            movespeed = baseSpeed / 2;

        }
        else
        {
            movespeed = baseSpeed;
        }

        rbd2.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal1") * movespeed, 0.8f),
                                        Mathf.Lerp(0, Input.GetAxis("Vertical1") * movespeed, 0.8f));
    }

 }