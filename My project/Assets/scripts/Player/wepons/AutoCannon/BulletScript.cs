using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletScript : Projectile
{
    public delegate void specialFunction(GameObject target, int damage, ref int scaledDamage);
    public specialFunction function;

    protected SpriteRenderer sr;
    protected Rigidbody2D rb;
    public GameObject impactEffect;
    public GameObject impactParticles;
    protected Transform target;
    protected Color32 Color;

    public float MaxBounceDistance;
    public bool OnImpactGunOnly;
    public bool OnImpact;
    public bool OnCrit;
    public bool PostImpact;

    protected List<Transform> Enemies = new List<Transform>();
    [HideInInspector] public List<GameObject> IgnoreTargets = new List<GameObject>();
    
    private void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        SetUpProjectile();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Player" && collision.isTrigger == false && IgnoreTargets.Contains(collision.gameObject) == false)
        {
            DealDamageToEmemy(collision);

            if (pierce <= 0)
            {
                if (Bounce > 0)
                {
                    Bounce = Bounce - 1;
                    Vector2 direction;
                   // Transform target;

                    Enemies.Add(collision.transform);
                    bool RandomCheck = false;
                    if(KnedlikLib.FindClosestEnemy(gameObject.transform,out target,Enemies))
                    {
                        Rigidbody2D rbTarget = target.GetComponent<Rigidbody2D>();
                        if (Vector3.Distance(target.position, transform.position) < MaxBounceDistance)
                        {
                            if (KnedlikLib.InterceptionPoint(target.position, transform.position, rbTarget.velocity, rb.velocity.magnitude, out direction))
                            {
                                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                                rb.rotation = angle;
                                rb.velocity = direction * rb.velocity.magnitude;
                            }
                            else
                            {
                                RandomCheck = true;
                            }
                        }else
                        {
                            RandomCheck = true;
                        }
                    }else
                    {
                        RandomCheck= true;
                    }

                    if (RandomCheck)
                    {
                        float rand1 = Random.Range(-100, 100) / 100f;
                        float rand2 = Random.Range(-100, 100) / 100f;
                        direction = new Vector2(rand1, rand2).normalized;
                        rb.velocity = direction * rb.velocity.magnitude;
                        Debug.Log(direction);

                        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                        rb.rotation = angle;
                    }
                    IgnoreTargets.Add(collision.gameObject);
                }
                else
                {
                    sr.enabled = false;
                    rb.velocity = Vector2.zero;

                    SpawnEffects();
                    Destroy(gameObject);
                }
            }

        }
    }

    public void SetUpProjectile()
    {
        rb = this.GetComponent<Rigidbody2D>();
        damage = damagePlus;
        Destroy(gameObject, destroyTime);
    }

    public void SpawnEffects()
    {
        if(impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
        }
        
        if (impactParticles != null)
        {
            Instantiate(impactParticles, transform.position, Quaternion.Euler(-90, 0, 0));
        }
    }

    public void DealDamageToEmemy(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();
        Rigidbody2D rbTarget = collision.GetComponent<Rigidbody2D>();
        Vector2 dir = (collision.transform.position - transform.position).normalized;

        if (health != null)
        {
            if (rbTarget != null && knockBack > 0)
            {
                StunOnHit stun = collision.GetComponent<StunOnHit>();
                if (stun != null)
                {
                    stun.Stun();
                }

                rbTarget.velocity = rbTarget.velocity.normalized;
                rbTarget.AddForce(dir * knockBack, ForceMode2D.Impulse);
            }

            if(function != null)
            {
                function(collision.gameObject, damage, ref damage);
            }

            if (eventManager.ImpactGunOnly != null && OnImpactGunOnly)
            {
                eventManager.ImpactGunOnly(collision.gameObject, gameObject);
            }

            if (eventManager.OnImpact != null && OnImpact)
            {
                eventManager.OnImpact(collision.gameObject, damage, ref damagePlus);
            }

            if (eventManager.OnCrit != null && OnCrit)
            {
                Color32 TempColor = eventManager.OnCrit(collision.gameObject, damagePlus, ref damagePlus);
                Color32 BaseColor = new Color32(0, 0, 0, 0);
                if (!TempColor.Equals(BaseColor))
                {
                    Color = TempColor;
                }
            }

            if (eventManager.PostImpact != null && PostImpact)
            {
                eventManager.PostImpact(collision.gameObject, damagePlus, ref damagePlus);
            }

            if (Color.Equals(new Color32(0, 0, 0, 0)))
            {
                health.TakeDamage(damagePlus);
            }
            else
            {
                //Debug.Log(Color);
                health.TakeDamage(damagePlus, Color);
            }

        }
    }
}
