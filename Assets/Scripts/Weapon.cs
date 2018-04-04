using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    private Player player;
    private bool attacking;
    private float attackPower;
    private BoxCollider2D col;

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        attacking = player.IsAttacking();
        attackPower = 10f;
        col = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        print("trigger");
        if (col.tag == "Enemy")
        {
            print("TRIGGER Weapon attacked " + col.name + " for " + attackPower + " damage.");
            col.GetComponent<Health>().Damage(attackPower);
        }
    }

    //private void OnCollisionEnter2D(Collision2D col)
    //{
    //    print("collision");
    //    if (col.gameObject.tag == "Enemy")
    //    {
    //        print("COLLISION Enemy " + col.gameObject.name + " attacked player for " + attackPower + " damage.");
    //        col.gameObject.GetComponent<Health>().Damage(attackPower);
    //    }
    //}

    public void SetColliderActive(bool x)
    {
        col.enabled = x;
    }
}
