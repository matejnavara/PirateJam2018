using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullEnemy : Enemy {

    public float smoothTime;
    public Vector3 smoothVelocity;
    private Animator anim;
    private bool dying;

    void Start () {
        anim = GetComponent<Animator>();
        moveRate = 20f;
        damageAmount = 3f;
        dying = false;
        smoothTime = 2.0f;
        smoothVelocity = Vector3.zero;
    }

    private void OnEnable()
    {
        dying = false;
        health.Respawn();
    }

    void FixedUpdate ()
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
            print(gameObject.name + " DIED");
            anim.SetTrigger("onDeath");
            dying = true;
        }
        
	}

    override public void Hit(float amount, Vector2 direction)
    {
        anim.SetTrigger("onHit");
        health.Damage(amount);
        rgdb.AddForce(direction * amount, ForceMode2D.Impulse);        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            print("Enemy " + gameObject.name + " attacked player for " + damageAmount + " damage.");
            player.Hit(damageAmount, Vector2.zero);
        }
    }

}
