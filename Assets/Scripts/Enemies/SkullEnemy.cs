using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullEnemy : Enemy {

    private Rigidbody2D rgdb;

    public float smoothTime;
    public Vector3 smoothVelocity;

    void Start () {
        rgdb = GetComponent<Rigidbody2D>();
        moveRate = 20f;
        damageAmount = 3f;

        smoothTime = 2.0f;
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
        if (col.gameObject.tag == "Player")
        {
            print("Enemy " + gameObject.name + " attacked player for " + damageAmount + " damage.");
            player.health.Damage(damageAmount);
        }
    }
}
