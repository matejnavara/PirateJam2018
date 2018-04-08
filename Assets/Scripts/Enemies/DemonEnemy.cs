using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonEnemy : Enemy {

    private Rigidbody2D rdb2;
    private Animator anim;

    public float smoothTime;
    public Vector3 smoothVelocity;
    private float walkAngle;
    private bool dying;


    void Start () {
        rdb2 = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        moveRate = 3f;
        damageAmount = 6f;
        smoothTime = 1.3f;
        smoothVelocity = Vector3.zero;
        dying = false;
    }

    private void OnEnable()
    {
        dying = false;
        anim.SetBool("alive", true );
        health.Respawn();
    }

    void FixedUpdate () {

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
            print(gameObject.name + " DIED");
            anim.SetBool("alive", false);
            dying = true;
        }       
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision between " + gameObject.name + " and " + col.gameObject.name + " with tag " + col.gameObject.tag);
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Enemy " + gameObject.name + " attacked player for " + damageAmount + " damage.");
            anim.SetTrigger("onAttack");
            Vector2 force = (col.transform.position - transform.position).normalized;
            player.Hit(damageAmount, force * 5f);
        }
    }
}
