using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilHealth : MonoBehaviour {

    private GameObject baby;
    private float maxHealth;
    private float health;
    private float healthBar;

	// Use this for initialization
	void Start () {

        baby = GameObject.FindGameObjectWithTag("Baby");
        maxHealth = baby.GetComponent<Health>().GetMaxHealth();
        health = baby.GetComponent<Health>().GetCurrentHealth();
        healthBar = 1.0f;
    }
	
	// Update is called once per frame
	void Update () {

        healthBar = baby.GetComponent<Health>().GetCurrentHealth() / maxHealth;
        if(healthBar > 0)
        {
            GetComponent<RectTransform>().localScale = new Vector3(1, healthBar);
        } else
        {
            GetComponent<RectTransform>().localScale = new Vector3(1, 0);
        }

    }
}
