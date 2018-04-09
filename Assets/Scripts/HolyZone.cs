using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyZone : MonoBehaviour {

    private Baby baby;

    public float enemyDamage;
    public float babyDamage;

    // Use this for initialization
    void Start () {

        baby = GameObject.FindGameObjectWithTag("Baby").GetComponent<Baby>();
        enemyDamage = 5f;
        babyDamage = 2f;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //private void OnTriggerStay2D(Collider2D col)
    //{
    //    if (col.tag == "Enemy")
    //    {
    //        col.GetComponent<Health>().Damage(enemyDamage);
    //    }

    //}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Baby")
        {
            baby.Exorcism(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Baby")
        {
            baby.Exorcism(false);
        }
    }
}
