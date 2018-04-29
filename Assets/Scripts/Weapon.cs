using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    private Player player;
    private BoxCollider2D col;
    private AudioSource audioSource;
    private float attackPower;
    private AudioClip playingSound;

    public AudioClip[] swingSounds;
    public AudioClip hitSound;

    // Use this for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        col = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        attackPower = 10f;
        SetColliderActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Trigger between " + gameObject.name + " and " + col.gameObject.name);
        if (col.tag == "Enemy")
        {
            audioSource.PlayOneShot(hitSound);
            Debug.Log("TRIGGER Weapon attacked " + col.name + " for " + attackPower + " damage.");
            Vector2 force = (col.transform.position - transform.position).normalized;
            col.GetComponent<Enemy>().Hit(attackPower, force);
        }

    }

    public void SetColliderActive(bool x)
    {
        col.enabled = x;
    }

    public void SwingSound()
    {
        if (!audioSource.isPlaying)
        {
            int index = Random.Range(0, swingSounds.Length);
            audioSource.clip = swingSounds[index];
            audioSource.Play();
        }
    }
}
