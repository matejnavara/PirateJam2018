using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    private GameObject player;
    private float maxHealth;
    private float health;
    private float healthBar;

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
        maxHealth = player.GetComponent<Health>().GetMaxHealth();
        health = player.GetComponent<Health>().GetCurrentHealth();
        healthBar = 1.0f;
    }
	
	// Update is called once per frame
	void Update () {

        healthBar = player.GetComponent<Health>().GetCurrentHealth() / maxHealth;
        if(healthBar > 0)
        {
            GetComponent<RectTransform>().localScale = new Vector3(1, healthBar);
        } else
        {
            GetComponent<RectTransform>().localScale = new Vector3(1, 0);
        }

    }
}
