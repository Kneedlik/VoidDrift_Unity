using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunPellet : BulletScript
{
    public int ClusterAmount;
    
    private void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
        //Debug.Log("destroyTime" + destroyTime);
        //Debug.Log("Pierce" + pierce);
        damage = damagePlus;
        Destroy(gameObject, destroyTime);
        SetUpProjectile();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Player" && collision.isTrigger == false && IgnoreTargets.Contains(collision.gameObject) == false)
        {
            DealDamageToEmemy(collision, damage);
            //Cluster();

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

                    if (ClusterAmount > 0)
                    {
                        Cluster();
                    }

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

    public void Cluster()
    {
        //Debug.Log("Clustering");
        projectileShotGun ShotGun = GameObject.FindWithTag("Weapeon").GetComponent<projectileShotGun>();
        float offset = 360f / ShotGun.ClusterProjectiles;
        float pom = offset;

        for (int i = 0; i < ShotGun.ClusterProjectiles; i++)
        {
            GameObject Pellet = Instantiate(ShotGun.pellet, transform.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + offset));

            ShotGunPellet PelletScript = Pellet.GetComponent<ShotGunPellet>();
            ShotGun.SetUpProjectile(PelletScript);
            PelletScript.ClusterAmount = ClusterAmount - 1;
            //PelletScript.IgnoreTargets.Add(Target);
            PelletScript.pierce = ShotGun.pierce + 1;
            PelletScript.destroyTime = ShotGun.ClusterAliveTime;
            float damagePom = ShotGun.damage * ShotGun.ClusterDamageMultiplier;
            PelletScript.setDamage((int)damagePom + ShotGun.extraDamage);
            PelletScript.setArea(ShotGun.size);
            
            Rigidbody2D rigidbody2D = Pellet.GetComponent<Rigidbody2D>();
            rigidbody2D.AddForce(Pellet.transform.up * ShotGun.Force,ForceMode2D.Impulse);
            offset += pom;
        }
        
    }
}
