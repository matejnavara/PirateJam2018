using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    private Player player;
    private float attackPower;
    private BoxCollider2D col;

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        attackPower = 10f;
        col = GetComponent<BoxCollider2D>();
        SetColliderActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Trigger between " + gameObject.name + " and " + col.gameObject.name);
        if (col.tag == "Enemy")
        {
            Debug.Log("TRIGGER Weapon attacked " + col.name + " for " + attackPower + " damage.");
            Vector2 force = (col.transform.position - transform.position).normalized;
            col.GetComponent<Enemy>().Hit(attackPower, force);
        }

    }

    public void SetColliderActive(bool x)
    {
        col.enabled = x;
    }
}
