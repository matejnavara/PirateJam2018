using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    public float damageAmount = 1f;    // just a default value
    public float moveRate = 10f;        // default value

    public Health health;
    public Player player;
    public Rigidbody2D rgdb;


    public void Awake()
    {
        health = GetComponent<Health>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rgdb = GetComponent<Rigidbody2D>();
    }

    virtual public void Hit(float amount, Vector2 direction)
    {
        health.Damage(amount);
        rgdb.AddForce(direction * amount, ForceMode2D.Impulse);
    }

    private void OnEnable()
    {
        health.Respawn();
    }
}