using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : Enemy {

    private Animator anim;

    void Start () {
        anim = GetComponent<Animator>();
        moveRate = 20f;
        damageAmount = 10f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if(health.GetCurrentHealth() > 0)
        {
            Vector2 heading = this.transform.position - player.transform.position;
            rgdb.AddForce(-heading * moveRate * Time.deltaTime);
        }
        else
        {
            print(gameObject.name + " DIED");
            anim.SetTrigger("onDeath");
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
            print("Enemy " + gameObject.name + " attacked player for " + damageAmount + " damage.");
            player.health.Damage(damageAmount);
        }
    }
}
