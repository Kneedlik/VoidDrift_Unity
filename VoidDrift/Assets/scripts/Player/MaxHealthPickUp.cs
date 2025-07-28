using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthPickUp : MonoBehaviour
{
    public int amount;

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            plaerHealth Health = GameObject.FindWithTag("Player").GetComponent<plaerHealth>();
            Health.setMaxHP(Health.maxHealth + amount);
            Health.increaseHP(Health.health + amount);

            Destroy(gameObject);
        }
    }
}
