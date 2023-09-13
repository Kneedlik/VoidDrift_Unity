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
        Destroy(gameObject, destroyTime);
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


            pierce = pierce - 1;

            if (pierce <= 0)
            {
                if (Bounce > 0)
                {
                    Bounce = Bounce - 1;
                    AutoCannon weapeon = GameObject.FindWithTag("Weapeon").GetComponent<AutoCannon>();
                    Vector2 direction;
                    Transform target;

                    if(KnedlikLib.FindClosestEnemy(gameObject.transform,out target))
                    {
                        Rigidbody2D rb2 = target.GetComponent<Rigidbody2D>();
                      
                        float angle;
                        if (KnedlikLib.InterceptionPoint(target.position, transform.position, rb2.velocity,weapeon.Force, out direction,out angle))
                        {
                            rb.rotation = angle;
                            rb.velocity = direction * weapeon.Force;
                        }else
                        {
                            float rand1 = Random.Range(0, 1);
                            float rand2 = Random.Range(0, 1);
                            direction = new Vector2(rand1, rand2);
                            rb.velocity = direction * weapeon.Force;
                        }
                    }else
                    {
                        float rand1 = Random.Range(0, 1);
                        float rand2 = Random.Range(0, 1);
                        direction = new Vector2(rand1, rand2);
                        rb.velocity = direction * weapeon.Force;
                    }
                }
                else
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
       
    }
}
