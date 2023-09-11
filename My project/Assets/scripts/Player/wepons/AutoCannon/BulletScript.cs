using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : Projectile
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    public GameObject impactEffect;
    public GameObject impactParticles;
    
    private void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
       damage = damagePlus ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Player" && collision.isTrigger == false)
        {
            Health health = collision.GetComponent<Health>();
             rb = collision.GetComponent<Rigidbody2D>();
            Vector2 dir = (collision.transform.position - transform.position).normalized;

            if (rb != null)
            {
                rb.AddForce(dir * knockBack, ForceMode2D.Impulse);
            }

            if (health != null)
            {
                if (eventManager.OnImpact != null)
                {
                    eventManager.OnImpact(collision.gameObject,damage,ref damagePlus);
                }

                if(eventManager.PostImpact != null)
                {
                    eventManager.PostImpact(collision.gameObject, damagePlus, ref damagePlus);
                }
                health.TakeDamage(damagePlus);
            }

            if (Bounce > 0)
            {
                Bounce--;
            }
            else
            {
                pierce--;

                if (pierce <= 0)
                {
                    sr.enabled = false;
                    rb.velocity = Vector2.zero;

                    Instantiate(impactEffect, transform.position, transform.rotation);
                    if (impactParticles != null)
                    {
                        Instantiate(impactParticles, transform.position, Quaternion.Euler(-90, 0, 0));
                    }
                    Destroy(gameObject);
                }

            }
        }
    }

    private void Update()
    {
        Destroy(gameObject, destroyTime);
    }
}
