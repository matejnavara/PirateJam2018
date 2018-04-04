using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    public float damageAmount = 1f;    // just a default value
    public float moveRate = 10f;        // default value

    public Health health;
    public Player player;
    //public Animator anim;


    public void Awake()
    {
        health = GetComponent<Health>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        health.Respawn();
    }
}