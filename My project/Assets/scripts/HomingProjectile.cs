using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : BulletScript
{
    public bool UseForce = false;
    public float force;
    public float MaxSpeed;
    public float rotSpeed;
    
    [SerializeField] bool Enemy = true;
    [SerializeField] bool Trigger = false;
    [SerializeField] GameObject ExploPrefab;

    [SerializeField] float Delay = 0;
    float TimeStamp;

    void Start()
    {
        if (Enemy)
        {
            target = GameObject.FindWithTag("Player").transform;
        }else
        {
            KnedlikLib.FindClosestEnemy(transform,out target);
        }
        rb = GetComponent<Rigidbody2D>();
        TimeStamp = Delay;
        Destroy(gameObject, destroyTime);
        SetUpProjectile();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeStamp <= 0)
        {
            if (target != null)
            {
                KnedlikLib.LookAtSmooth(transform,target.position,rotSpeed);
            }
            else
            {
                if (!Enemy)
                {
                    KnedlikLib.FindClosestEnemy(transform, out target);
                }
            }
        }else
        {
            TimeStamp -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if(UseForce)
        {
            rb.AddForce(transform.up * force, ForceMode2D.Force);
        }else
        {
            rb.velocity = transform.up * force;
        }
        
        if(MaxSpeed != 0)
        {
            KnedlikLib.SetMaxSpeed(MaxSpeed, rb);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!Trigger)
        {
            if (Enemy)
            {
                if (collision.transform.tag != "Enemy")
                {
                    if (collision.transform.tag == "Player")
                    {
                        if (damage > 0)
                        {
                            collision.gameObject.GetComponent<plaerHealth>().TakeDamage(damage);
                        }
                    }

                    rb.velocity = Vector2.zero;

                    DropLootOnDeath DropLoot = GetComponent<DropLootOnDeath>();
                    if(DropLoot != null)
                    {
                        DropLoot.DropLoot();
                    }
                    Destroy(gameObject);
                }
            }
            else
            {
                if (collision.transform.tag != "Player")
                {
                    if (damage > 0)
                    {
                        Health health = collision.gameObject.GetComponent<Health>();
                        if (health != null)
                        {
                            health.TakeDamage(damage);
                        }

                    }

                    if(ExploPrefab != null)
                    {
                        Instantiate(ExploPrefab, transform.position, Quaternion.identity);
                    }
                    Destroy(gameObject);
                }


            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            if (Trigger)
            {
                if (Enemy)
                {
                    if (collision.transform.tag != "Enemy")
                    {
                        if (collision.transform.tag == "Player")
                        {
                            if (damage > 0)
                            {
                                collision.gameObject.GetComponent<plaerHealth>().TakeDamage(damage);
                            }
                        }else
                        {
                            Health health = collision.gameObject.GetComponent<Health>();
                            if(health != null && damage > 0)
                            {
                                health.TakeDamage(damage);
                            }
                        }

                        rb.velocity = Vector2.zero;

                        if (ExploPrefab != null)
                        {
                            Instantiate(ExploPrefab, transform.position, Quaternion.identity);
                        }
                        Destroy(gameObject);
                    }
                }
                else
                {
                    if (collision.transform.tag != "Player")
                    {
                        DealDamageToEmemy(collision,damage);

                        if(ExploPrefab != null)
                        {
                            Instantiate(ExploPrefab, transform.position, Quaternion.identity);
                        }
                        
                        Destroy(gameObject);
                    }


                }
            }
        }
    }

}
