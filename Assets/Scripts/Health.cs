using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float maxHealth = 100f;
    public float currentHealth;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
    }
	
    public float GetMaxHealth()
    {
        return maxHealth;
    }

	public void SetMaxHealth(float amount)
    {
        maxHealth = amount;
    }

    public float GetCurrentHealth() 
    {
        return currentHealth;
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
    }

    public void Heal(float amount)
    {
        if((currentHealth + amount) <= maxHealth)
        {
            currentHealth += amount;
        } else
        {
            currentHealth = maxHealth;
        }
        
    }

    public void Respawn()
    {
        currentHealth = maxHealth;
    }

    public void Die()
    {
        gameObject.GetComponent<PooledObject>().ReturnObject();
    }

}
