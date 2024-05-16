using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunPellet : BulletScript
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    Transform target;
    Color32 Color;
    List<Transform> Enemies = new List<Transform>();

    public int ClusterAmount;
    public GameObject IgnoreTarget;
    projectileShotGun ShotGun;
    

    private void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
        damage = damagePlus;
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Player" && collision.isTrigger == false && collision.gameObject != IgnoreTarget)
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


                if (eventManager.ImpactGunOnly != null)
                {
                    eventManager.ImpactGunOnly(collision.gameObject, gameObject);
                }

                if (eventManager.OnImpact != null)
                {
                    eventManager.OnImpact(collision.gameObject, damage, ref damagePlus);
                }

                if (eventManager.OnCrit != null)
                {
                    Color32 TempColor = eventManager.OnCrit(collision.gameObject, damagePlus, ref damagePlus);
                    Color32 BaseColor = new Color32(0, 0, 0, 0);
                    if (!TempColor.Equals(BaseColor))
                    {
                        Color = TempColor;
                    }
                }

                if (eventManager.PostImpact != null)
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

            if (pierce <= 0)
            {
                if (Bounce > 0)
                {
                    Bounce = Bounce - 1;
                    Vector2 direction;
                    // Transform target;

                    Enemies.Add(collision.transform);
                    bool RandomCheck = false;
                    if (KnedlikLib.FindClosestEnemy(gameObject.transform, out target, Enemies))
                    {
                        rbTarget = target.GetComponent<Rigidbody2D>();
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
                        }
                        else
                        {
                            RandomCheck = true;
                        }
                    }
                    else
                    {
                        RandomCheck = true;
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
                }
                else
                {
                    sr.enabled = false;
                    rb.velocity = Vector2.zero;

                    Cluster(collision.gameObject);
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

    public void Cluster(GameObject Target)
    {
        float offset = 360f / ShotGun.ClusterProjectiles;
        float pom = offset;

        for (int i = 0; i < ShotGun.ClusterProjectiles; i++)
        {
            GameObject Pellet = Instantiate(ShotGun.pellet, transform.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + offset));

            ShotGunPellet PelletScript = Pellet.GetComponent<ShotGunPellet>();
            ShotGun.SetUpProjectile(PelletScript);
            PelletScript.ClusterAmount = ClusterAmount - 1;
            PelletScript.IgnoreTarget = Target;
            PelletScript.destroyTime = ShotGun.ClusterAliveTime;
            float damagePom = PelletScript.damage * ShotGun.ClusterDamageMultiplier;
            PelletScript.damage = (int)damagePom;
            
            Rigidbody2D rigidbody2D = Pellet.GetComponent<Rigidbody2D>();
            rigidbody2D.AddForce(Pellet.transform.up * ShotGun.Force);
            offset += pom;
        }
        
    }
}
