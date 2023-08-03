using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    public delegate void specialFunction(GameObject target, int damage, ref int scaledDamage);
    public specialFunction function;

    public int damage;
    public int reducedDamage;
    public float destroyTime;
    public float force;
    public bool isEnemy = false;
    

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 dir = (collision.transform.position - transform.position).normalized;

        if(force > 0)
        {
            if (collision.isTrigger == false)
            {
                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
                rb.AddForce(dir * force, ForceMode2D.Impulse);
            }
        }

        if(function != null)
        {
            function(collision.gameObject, damage, ref damage);
        }
        
        Health health = collision.GetComponent<Health>();
        plaerHealth pHealth = collision.GetComponent<plaerHealth>();

        if(isEnemy)
        {
            if(pHealth != null)
            {
                pHealth.TakeDamage(damage);
            }

            if (health != null)
            {
                health.TakeDamage(reducedDamage);
            }
        }
        else
        {
            if (pHealth != null)
            {
                pHealth.TakeDamage(reducedDamage);
            }

            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }

       
    }


}
