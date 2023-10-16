using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpininigSummonOrb : MonoBehaviour
{
    public int damage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        for (int i = 0; i < enemies.Count; i++)
        {
           if (enemies[i] == null)
           {
                enemies.RemoveAt(i);
                timestamps.RemoveAt(i);
            }
        }
      
        for (int i = 0; i < timestamps.Count; i++)
        {
           timestamps[i] -= Time.deltaTime;
            if(timestamps[i] <= 0)
            {
                timestamps.RemoveAt(i);
                if (enemies[i] != null)
                {
                    enemies.RemoveAt(i);
                }
            }
       
        }
       */ 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();
        if(health != null)
        {
            //if(enemies.Contains(collision.gameObject) == false)
           //{
            int Damage = damage;
           // enemies.Add(collision.gameObject);
               // float time = coolDown;
               // timestamps.Add(time);

            if (eventManager.OnImpact != null)
            {
                eventManager.OnImpact(collision.gameObject, damage, ref Damage);
            }

            if (eventManager.PostImpact != null)
            {
                eventManager.PostImpact(collision.gameObject, Damage, ref Damage);
            }
               
            health.TakeDamage(Damage);
            //}
        }
    }

}
