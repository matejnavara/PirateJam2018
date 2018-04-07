using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : Enemy {

    private Rigidbody2D rgdb;

	void Start () {
        rgdb = GetComponent<Rigidbody2D>();
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
            gameObject.GetComponent<PooledObject>().ReturnObject();
        }
        
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
