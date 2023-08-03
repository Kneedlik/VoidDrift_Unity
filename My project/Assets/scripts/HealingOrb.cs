using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingOrb : MonoBehaviour
{
     plaerHealth health;
     healthBar healthBar;
    int maxHealth;
   

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

            maxHealth = health.maxHealth;

            health.health = maxHealth;
            healthBar.SetHealth(maxHealth);
            Destroy(gameObject);

        }
    }
}
