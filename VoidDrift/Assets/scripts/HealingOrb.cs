using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingOrb : MonoBehaviour
{
    public float HealthRestore;
    plaerHealth health;
    healthBar healthBar;

   // private void OnApplicationQuit()
   // {
   //     isQuit = true;
   // }

  /*  private void OnDestroy()
    {
        if(!isQuit)
        {
            health = GameObject.FindWithTag("Player").GetComponent<plaerHealth>();
            healthBar = GameObject.FindWithTag("HealthBar").GetComponent<healthBar>();

            maxHealth = health.maxHealth;
            
           health.health = maxHealth;
           healthBar.SetHealth(maxHealth);
        }
    }
  */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            health = collision.GetComponent<plaerHealth>();

            healthBar = GameObject.FindWithTag("HealthBar").GetComponent<healthBar>();

            float HealthTemp = health.maxHealth * HealthRestore;
            for (int i = 0; i < HealthTemp; i++)
            {
                if (health.health < health.maxHealth)
                {
                    health.health = health.health + 1;
                    healthBar.SetHealth(health.health);
                }
            }
            Destroy(gameObject);
        }
    }
}
