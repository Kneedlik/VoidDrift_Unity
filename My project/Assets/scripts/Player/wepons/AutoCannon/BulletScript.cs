using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : Projectile
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    public GameObject impactEffect;
    public GameObject impactParticles;
    Transform target;
    
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
            Rigidbody2D rbTarget = collision.GetComponent<Rigidbody2D>();
            Vector2 dir = (collision.transform.position - transform.position).normalized;

            if (rbTarget != null)
            {
                rbTarget.AddForce(dir * knockBack, ForceMode2D.Impulse);
            }

            if (health != null)
            {
                if (eventManager.ImpactGunOnly != null)
                {
                    eventManager.ImpactGunOnly(collision.gameObject, gameObject);
                }

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


            if (pierce <= 0)
            {
                if (Bounce > 0)
                {
                    Bounce = Bounce - 1;
                    Vector2 direction;
                   // Transform target;

                    List<Transform> transforms = new List<Transform>();
                    transforms.Add(collision.transform);
                    if(KnedlikLib.FindClosestEnemy(gameObject.transform,out target,transforms))
                    {
                        rbTarget = target.GetComponent<Rigidbody2D>();
                      
                        
                        if (KnedlikLib.InterceptionPoint(target.position, transform.position, rbTarget.velocity,rb.velocity.magnitude, out direction))
                        {
                            // rb.
                            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                            rb.rotation = angle;
                            rb.velocity = direction * rb.velocity.magnitude;
                        }else
                        {
                            float rand1 = Random.Range(0, 1);
                            float rand2 = Random.Range(0, 1);
                            direction = new Vector2(rand1, rand2);
                            rb.velocity = direction * rb.velocity.magnitude;
                        }
                    }else
                    {
                        float rand1 = Random.Range(0, 1);
                        float rand2 = Random.Range(0, 1);
                        direction = new Vector2(rand1, rand2);
                        rb.velocity = direction * rb.velocity.magnitude;
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
