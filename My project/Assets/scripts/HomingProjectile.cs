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
    [SerializeField] bool PlayerOnly = false;
    [SerializeField] bool Trigger = false;
    [SerializeField] GameObject ExploPrefab;

    [SerializeField] float Delay = 0;
    [SerializeField] float StopAfter;
    [SerializeField] float StartForceAfter;
    float TimeStamp;
    float TimeStamp2;
    float TimeStamp3;
    bool Locked;
    float ExploArea = 1;

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
        TimeStamp2 = StopAfter;
        TimeStamp3 = StartForceAfter;
        Destroy(gameObject, destroyTime);
        SetUpProjectile();
    }

    // Update is called once per frame
    void Update()
    {
        if (StopAfter != 0)
        {
            if (TimeStamp2 > 0)
            {
                TimeStamp2 -= Time.deltaTime;
            }else
            {
                Locked = true;
            }
        }

        if (StartForceAfter != 0)
        {
            if(TimeStamp3 > 0)
            {
                TimeStamp3 -= Time.deltaTime;
            }
        }

        if (Locked)
        {
            return;
        }

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
        if(TimeStamp3 > 0)
        {
            return;
        }

        if (force != 0)
        {
            if (UseForce)
            {
                rb.AddForce(transform.up * force, ForceMode2D.Force);
            }
            else
            {
                rb.velocity = transform.up * force;
            }
        }
        
        if(MaxSpeed != 0)
        {
            KnedlikLib.SetMaxSpeed(MaxSpeed, rb);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(ExploArea == 0)
        {
            ExploArea = 1;
        }

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
                        bool TargetFound = false;

                        if (collision.transform.tag == "Player")
                        {
                            if (damage > 0)
                            {
                                collision.gameObject.GetComponent<plaerHealth>().TakeDamage(damage);
                                TargetFound = true;
                            }
                        }else if(PlayerOnly == false)
                        {
                            Health health = collision.gameObject.GetComponent<Health>();
                            if(health != null && damage > 0)
                            {
                                health.TakeDamage(damage);
                                TargetFound = true;
                            }
                        }

                        if (TargetFound)
                        {
                            rb.velocity = Vector2.zero;
                            if (ExploPrefab != null)
                            {
                                Instantiate(ExploPrefab, transform.position, Quaternion.identity);
                            }
                            Destroy(gameObject);
                        }
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

    public void SetTotalArea(float Size, bool IncludeParent)
    {
        if(IncludeParent)
        {
            setArea(Size);
        }
        ExploArea = Size;
    }

}
