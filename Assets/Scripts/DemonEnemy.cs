using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonEnemy : Enemy {

    private Rigidbody2D rgdb;

    public float smoothTime;
    public Vector3 smoothVelocity;

    void Start () {
        rgdb = GetComponent<Rigidbody2D>();
        moveRate = 3f;
        damageAmount = 6f;

        smoothTime = 1.3f;
        smoothVelocity = Vector3.zero;
    }

    

    void FixedUpdate () {

        if(health.GetCurrentHealth() > 0)
        {
            //transform.LookAt(player.transform);
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < moveRate)
            {
                transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref smoothVelocity, smoothTime);
            }
        }
        else
        {
            print(gameObject.name + " DIED");
            gameObject.GetComponent<PooledObject>().ReturnObject();
        }
        
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision between " + gameObject.name + " and " + col.gameObject.name + " with tag " + col.gameObject.tag);
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Enemy " + gameObject.name + " attacked player for " + damageAmount + " damage.");
            player.health.Damage(damageAmount);
            Vector2 force = (col.transform.position - transform.position).normalized;
            print("applying force: " + force);
            player.rbd2.AddForce(force * 200f, ForceMode2D.Impulse);
        }
    }
}
