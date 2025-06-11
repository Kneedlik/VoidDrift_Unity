using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foddeBullet : MonoBehaviour
{
    public int damage = 10;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    public float destroyTime;
    public GameObject impactEffect;
    public GameObject impactParticles;
    public int pierce = 0;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Enemy" && collision.isTrigger == false)
        {
            if (collision.transform.tag == "Player")
            {
                collision.gameObject.GetComponent<plaerHealth>().TakeDamage(damage);
            }else
            {
                Health health = collision.GetComponent<Health>();
                if(health != null)
                {
                    health.TakeDamage(damage);
                }
            }

            if(pierce <= 0)
            {
                if (sr != null)
                {
                    sr.enabled = false;
                }

                rb.velocity = Vector2.zero;

                if(impactEffect != null)
                {
                    Instantiate(impactEffect, transform.position, transform.rotation);
                }

                if (impactParticles != null)
                {
                    Instantiate(impactParticles, transform.position, Quaternion.Euler(-90, 0, 0));
                }
                Destroy(gameObject);
            }else
            {
                pierce--;
            }

            
        }
    }

    private void Update()
    {
        Destroy(gameObject, destroyTime);
    }
}
